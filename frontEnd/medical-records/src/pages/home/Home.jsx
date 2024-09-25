import Card from "./Card";

/* eslint-disable react/prop-types */
export default function Home(props) {
  const { name } = props;
  console.log(name);
  return (
    <div style={{ display: "flex", justifyContent: "center" }}>
      {name ? (
        <>
          <Card />
        </>
      ) : (
        <h1>You are not logged in</h1>
      )}
    </div>
  );
}
