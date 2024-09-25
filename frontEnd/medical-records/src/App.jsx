import { BrowserRouter } from "react-router-dom";
import { Routes } from "react-router-dom";
import { Route } from "react-router-dom";
import "./App.css";
import Nav from "./components/navigation/Nav";
import Login from "./pages/login/Login";
import Register from "./pages/register/Register";
import Home from "./pages/home/Home";
import { useEffect, useState } from "react";
import Patients from "./pages/patient";

function App() {
  const [name, setName] = useState("Weimar");

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch("XXXXXXXXXXXXXXXXXXXXXXXXXX", {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          },
          credentials: "include",
        });

        const content = await response.json();

        setName(content);
      } catch (error) {
        console.error("Error fetching data:", error);
        // Handle the error here, e.g., display an error message or retry the fetch
      }
    };

    fetchData();
  }, []);

  return (
    <div className="App">
      <BrowserRouter>
        <Nav name={name} />
        <main className="form-signin">
          <Routes>
            <Route path="/" exact element={<Home name={name} />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/patients" element={<Patients />} />
          </Routes>
        </main>
      </BrowserRouter>
    </div>
  );
}

export default App;
