using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MedicalRecords.Configurations;
using MedicalRecords.DTOS;
using MedicalRecords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using RestSharp.Authenticators;

namespace MedicalRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        //private readonly JwtConfig _jwtConfig;

        public AuthenticationController(UserManager<IdentityUser> userManager,
            IConfiguration configuration)
            //JwtConfig jwtConfig)
        {
            _userManager = userManager;
            _configuration = configuration;
           // _jwtConfig = jwtConfig;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        {
            //Validate the incoming request
            if (ModelState.IsValid)
            {
                //we need to check if the email already exists
                var user_exists = await _userManager.FindByEmailAsync(requestDto.Email!);

                if(user_exists != null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Email already exists"
                        }
                    });
                }

                //Create a user

                var new_user = new IdentityUser()
                {
                    Email = requestDto.Email,
                    UserName = requestDto.Name,
                    EmailConfirmed = false
                };

                var is_created = await _userManager.CreateAsync(new_user, requestDto.Password!);

                if(is_created.Succeeded)
                {
                    
                    //Generate JWT token
                    var jwt_token = GenerateJwtToken(new_user);

                    return Ok(new AuthResult()
                    {
                        Result = true,
                        Token = jwt_token
                    });
                }

                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Server Error!"
                    },
                    Result = false
                });
            }

            return BadRequest(ModelState);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto loginRequest)
        {
            if(ModelState.IsValid)
            {
                //Check if the user exists
                var existing_user = await _userManager.FindByEmailAsync(loginRequest.Email!);

                if(existing_user == null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid email"
                        }
                    });
                }

                //Check if the password is correct

                var isPasswordCorrect = await _userManager.CheckPasswordAsync(existing_user, loginRequest.Password!);

                if(!isPasswordCorrect)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid password"
                        }
                    });
                }

                //Generate JWT token

                var jwtToken = GenerateJwtToken(existing_user);

                Response.Cookies.Append("jwtToken", jwtToken, new CookieOptions
                {
                    HttpOnly = true,
                });

                return Ok(new AuthResult() 
                {
                    Token = jwtToken,
                    Result = true
                });
            }

            return BadRequest(new AuthResult()
            {
                Result = false,
                Errors = new List<string>()
                {
                    "Invalid Credentials"
                }
            });
        }

        [Route("Logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken");

            return Ok(new AuthResult()
            {
                Result = true
            });
        }

        [Route("user")]
        [HttpGet]
        public ActionResult RegistratedUser() {
             var jwt = Request.Cookies["jwtToken"];

             if (jwt == null) {
                 return Unauthorized();
             }

             var token = ReadToken(jwt!);

             string userId = token.Issuer;

             var user = _userManager.FindByIdAsync(userId);

             var emailClaim = token.Claims.FirstOrDefault(c => c.Type == "email");

            string? email = emailClaim?.Value;

            if (email != null)
            {
                // Use the email value
                Console.WriteLine("Email: " + email);
            }
            else
            {
                Console.WriteLine("Email claim not found in the token.");
            }

             return Ok(email);
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value!);

            //Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Email, value: user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public JwtSecurityToken ReadToken(string token)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value!);

            var parameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            jwtTokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
