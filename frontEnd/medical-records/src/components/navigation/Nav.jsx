/* eslint-disable react/prop-types */
import LoggedIn from "./layouts/LoggedIn";
import LoggedOut from "./layouts/LoggedOut";

export default function Nav(props) {
  const { name } = props;
  console.log("nav", name);
  return (
    <nav className="navbar navbar-expand-md navbar-dark bg-dark mb-4">
      {name ? <LoggedIn /> : <LoggedOut />}
    </nav>
  );
}
