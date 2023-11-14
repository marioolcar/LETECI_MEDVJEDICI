import { createBrowserRouter } from "react-router-dom";
import React from "react";
import { Login } from "./components/Login/Login";
import { Register } from "./components/Register/Register";
import { Appbar } from "./components/Appbar/Appbar";

export const router = createBrowserRouter([
  {
    path: "login",
    element: <Login />,
  },
  {
    path: "register",
    element: <Register />,
  },
  {
    path: "/",
    element: <Appbar />,
  },
]);
