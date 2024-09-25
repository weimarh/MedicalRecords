import { NavLink } from "react-router-dom";

export default function LoggedIn() {
  return (
    <div className="container-fluid">
      <div>
        <ul className="navbar-nav me-auto mb-2 mb-md-0">
          <li className="nav-item">
            <NavLink
              to="/logout"
              className="nav-link active"
              aria-current="page"
            >
              Menu
            </NavLink>
          </li>
        </ul>
      </div>
      <div>
        <ul className="navbar-nav me-auto mb-2 mb-md-0">
          <li className="nav-item">
            <NavLink
              to="/logout"
              className="nav-link active"
              aria-current="page"
            >
              Logout
            </NavLink>
          </li>
        </ul>
      </div>
    </div>
  );
}
