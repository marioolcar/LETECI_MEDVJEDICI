import { createBrowserRouter } from "react-router-dom";
import React from "react";
import { Login } from "./components/Login/Login";
import { Register } from "./components/Register/Register";
import { Appbar } from "./components/Appbar/Appbar";
import App from "./App";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: "login",
        element: <Login />,
      },
      {
        path: "register",
        element: <Register />,
      },
    ]
  },
]);
