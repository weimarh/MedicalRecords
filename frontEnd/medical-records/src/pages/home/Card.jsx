import { NavLink } from "react-router-dom";
import items from "./items.json";

export default function Card() {
  return (
    <div
      style={{
        display: "flex",
        flexDirection: "row",
        justifyContent: "center",
        alignItems: "center",
        gap: "20px",
      }}
    >
      {items.map((item) => (
        <NavLink key={item.id} to={item.goto}>
          <div className="card" style={{ height: "200px" }}>
            <img src={item.link} className="card-img-top" alt={item.title} />
            <div className="card-body" style={{ textAlign: "center" }}>
              <h5 className="card-title">{item.title}</h5>
            </div>
          </div>
        </NavLink>
      ))}
    </div>
  );
}
