import { Button, Stack } from "@mui/material";
import React, { useState } from "react";
import { redirect, useNavigate } from "react-router-dom";
import { Login } from "../../components/Login/Login";
import { Register } from "../../components/Register/Register";

interface WelcomeScreenProps {
  setJwtToken: any;
}

export function WelcomeScreen({ setJwtToken }: WelcomeScreenProps) {
  const [openLoginModal, setOpenLoginModal] = useState<boolean>(false);
  const [openRegisterModal, setOpenRegisterModal] = useState<boolean>(false);

  const navigate = useNavigate();
  const handleLoginClick = () => {
    setOpenLoginModal(true);
    navigate("/login");
  };
  const handleLoginClose = () => {
    setOpenLoginModal(false);
    navigate("/");
  };
  const handleRegisterClick = () => {
    setOpenRegisterModal(true);
    navigate("/register");
  };
  const handleRegisterClose = () => {
    setOpenRegisterModal(false);
    navigate("/");
  };
  return (
    <Stack
      direction="row"
      justifyContent="center"
      alignItems="center"
      spacing={2}
      height="100vh"
    >
      <Stack>
        <Button variant="outlined" size="large" onClick={handleLoginClick}>
          LOGIN
        </Button>
      </Stack>
      <Stack>
        <Button variant="contained" size="large" onClick={handleRegisterClick}>
          REGISTRACIJA
        </Button>
      </Stack>
      <Login
        openLoginModal={openLoginModal}
        handleClose={handleLoginClose}
        setJwtToken={setJwtToken}
      />
      <Register
        openRegisterModal={openRegisterModal}
        handleClose={handleRegisterClose}
      />
    </Stack>
  );
}
