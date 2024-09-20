import { useState } from "react";
import { Navigate } from "react-router-dom";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [redirectTo, setRedirectTo] = useState(false);

  const onSubmit = async (e) => {
    e.preventDefault();

    try {
      await fetch("http://localhost:5039/api/authentication/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify({
          email,
          password,
        }),
      });
    } catch (error) {
      console.error(error);
    }

    setRedirectTo(true);
  };

  if (redirectTo) {
    return <Navigate to="/" />;
  }

  return (
    <form onSubmit={onSubmit}>
      <h1 className="h3 mb-3 fw-normal">Please sign in</h1>

      <div className="form-floating">
        <input
          type="email"
          className="form-control"
          placeholder="name@example.com"
          required
          onChange={(e) => setEmail(e.target.value)}
        />
        <label>Email address</label>
      </div>
      <div className="form-floating">
        <input
          type="password"
          className="form-control"
          placeholder="Password"
          required
          onChange={(e) => setPassword(e.target.value)}
        />
        <label>Password</label>
      </div>
      <button className="btn btn-primary w-100 py-2" type="submit">
        Sign in
      </button>
    </form>
  );
}
