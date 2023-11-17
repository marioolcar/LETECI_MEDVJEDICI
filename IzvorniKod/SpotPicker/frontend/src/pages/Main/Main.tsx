import { Stack, Typography } from "@mui/material";
import React from "react";
import { Appbar } from "../../components/Appbar/Appbar";

interface MainProps {
  setJwtToken: any;
}

export function Main({ setJwtToken }: MainProps) {
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
          Uspješno ste se ulogirali!
        </Typography>
      </Stack>
    </>
  );
}
