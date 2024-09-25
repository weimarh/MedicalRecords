import { NavLink } from "react-router-dom";

export default function LoggedOut() {
  return (
    <div className="container-fluid">
      <NavLink to="/" className="navbar-brand">
        Home
      </NavLink>
      <div>
        <ul className="navbar-nav me-auto mb-2 mb-md-0">
          <li className="nav-item">
            <NavLink to="/login" className="nav-link" aria-current="page">
              Login
            </NavLink>
          </li>
          <li className="nav-item">
            <NavLink to="/register" className="nav-link">
              Register
            </NavLink>
          </li>
        </ul>
      </div>
    </div>
  );
}
