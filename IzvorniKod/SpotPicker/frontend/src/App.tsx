import React, { useEffect, useState } from "react";
import "./App.css";
import { Appbar } from "./components/Appbar/Appbar";
import { Login } from "./components/Login/Login";
import { Link, redirect } from "react-router-dom";
import { Register } from "./components/Register/Register";
import { CssBaseline, ThemeProvider, createTheme } from "@mui/material";
import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { Main } from "./pages/Main/Main";
import { WelcomeScreen } from "./pages/WelcomeScreen/WelcomeScreen";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const theme = createTheme({
  palette: {
    mode: "dark",
  },
});
function App() {
  const [jwtToken, setJwtToken] = useState<string | null>(null);
  useEffect(() => {
    if (localStorage.getItem("jwt-token")) {
      setJwtToken(localStorage.getItem("jwt-token"));
    } else {
      setJwtToken(null);
    }
    console.log(jwtToken);
  }, [localStorage.getItem("jwt-token"), jwtToken]);
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      {jwtToken ? (
        <Main setJwtToken={setJwtToken} />
      ) : (
        <WelcomeScreen setJwtToken={setJwtToken} />
      )}
      <ToastContainer />
    </ThemeProvider>
  );
}

export default App;
