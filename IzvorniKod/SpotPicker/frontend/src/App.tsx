import React, { useState } from "react";
import "./App.css";
import { Appbar } from "./components/Appbar/Appbar";
import { Login } from "./components/Login/Login";
import { Link, redirect } from "react-router-dom";
import { Register } from "./components/Register/Register";

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  return <Register />;
}

export default App;
