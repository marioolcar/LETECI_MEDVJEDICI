import React, { useState } from "react";
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

const theme = createTheme({
  palette: {
    mode: "dark",
  },
  
});
function App() {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      {
        isLoggedIn ? <Main /> : <WelcomeScreen />
      }
    </ThemeProvider>
  );
}

export default App;
