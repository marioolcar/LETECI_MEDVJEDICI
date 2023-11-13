import React, { useState } from "react";
import "./App.css";
import { Appbar } from "./components/Appbar/Appbar";
import { Login } from "./components/Login/Login";

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  return <>{isLoggedIn ? <Appbar /> : <Login />}</>;
}

export default App;
