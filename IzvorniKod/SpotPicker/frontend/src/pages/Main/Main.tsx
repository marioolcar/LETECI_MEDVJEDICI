import { Button } from "@mui/material";
import React from "react";
import { useNavigate } from "react-router-dom";

interface MainProps {
  setJwtToken: any;
}

export function Main({ setJwtToken }: MainProps) {
  const handleClick = () => {
    localStorage.removeItem("jwt-token");
    setJwtToken(null);
  };
  return (
    <>
      <h1>You are logged in</h1>
      <Button variant="contained" onClick={handleClick}>
        Log out
      </Button>
    </>
  );
}
