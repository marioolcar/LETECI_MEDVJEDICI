import { Button, Stack, Typography } from "@mui/material";
import React from "react";
import { useNavigate } from "react-router-dom";
import { useUser } from "../../contexts/UserContext";
import { Appbar } from "../../components/Appbar/Appbar";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

interface MainProps {
  setJwtToken: any;
}

export function Main({ setJwtToken }: MainProps): JSX.Element {
  const { jwtToken, username } = useUser();
  console.log("Username from Main component:", username);

  const handleClick = () => {
    localStorage.removeItem("jwt-token");
    setJwtToken(null);
  };
  return (
    <>
      <Appbar setJwtToken={setJwtToken} />
      <Stack
        direction="row"
        justifyContent="center"
        alignItems="center"
        sx={{ marginTop: "2em" }}
      >
        <Typography variant="h5" gutterBottom>
          Dobrodošli {username}!
        </Typography>
      </Stack>
    </>
  );
}