import { Button, Grid, Stack, TextField, Typography } from "@mui/material";
import React, { useState } from "react";
import { useUser } from "../../contexts/UserContext";
import { Appbar } from "../../components/Appbar/Appbar";
import 'react-toastify/dist/ReactToastify.css';

interface MainProps {
  setJwtToken: any;
}

export function Main({ setJwtToken }: MainProps): JSX.Element {
  const { jwtToken, username } = useUser();
  const [walletAmount, setWalletAmount] = useState<number>(0);
  const [totalMoney, setTotalMoney] = useState<number>(0);

  const handleClick = () => {
    localStorage.removeItem("jwt-token");
    setJwtToken(null);
  };

  const handleAddToWallet = () => {
    setTotalMoney(totalMoney + walletAmount);
    setWalletAmount(0);
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
        <Stack
        direction="column"
        justifyContent="flex-end"
        alignItems="center"
        sx={{
          position: 'fixed',
          bottom: 0,
          width: '100%',
          marginBottom: '2em',
          padding: '1em',
          borderTop: '1px solid #ccc',
        }}
      >
        <Grid container spacing={3} alignItems="center">
          <Grid item>
            <Typography variant="body1">Ukupni novac: {totalMoney} €</Typography>
          </Grid>
          <Grid item>
            <TextField
              label="Unesite iznos"
              variant="outlined"
              type="number"
              size="small"
              value={walletAmount}
              onChange={(e) => setWalletAmount(Number(e.target.value))}
            />
          </Grid>
          <Grid item>
            <Button variant="contained" onClick={handleAddToWallet}>
              Dodaj Novac
            </Button>
          </Grid>
        </Grid>
      </Stack>
      </Stack>
    </>
  );
}