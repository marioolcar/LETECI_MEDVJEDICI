import { Button, Grid, Modal, Paper, Stack, TextField, Typography } from "@mui/material";
import React, { useState } from "react";
import { useUser } from "../../contexts/UserContext";
import { Appbar } from "../../components/Appbar/Appbar";
import 'react-toastify/dist/ReactToastify.css';
import { getAllKorisnici } from "../../services/BackendService";
import { getAllKorisniciForApproval } from "../../services/BackendService";

interface MainProps {
  setJwtToken: any;
}

interface Korisnik {
  id: number;
  username: string;
  password: string;
  razinaPristupa: number;
  name: string;
  surname: string;
  pictureData: string;
  bankAccountNumber: string;
  email: string;
}

export function Main({ setJwtToken }: MainProps): JSX.Element {
  const { jwtToken, username } = useUser();
  const [walletAmount, setWalletAmount] = useState<number>(0);
  const [totalMoney, setTotalMoney] = useState<number>(0);
  const [userList, setUserList] = useState<Korisnik[]>([]);
  const [showUserList, setShowUserList] = useState(false);
  const [showPendingLeadersModal, setShowPendingLeadersModal] = useState(false);
  const [pendingLeaders, setPendingLeaders] = useState<Korisnik[]>([]);



  const handleClick = () => {
    localStorage.removeItem("jwt-token");
    setJwtToken(null);
  };

  const handleAddToWallet = () => {
    setTotalMoney(totalMoney + walletAmount);
    setWalletAmount(0);
  };

  const handleToggleUserList = async () => {
    if (username === 'Admin') {
      try {
        const response = await getAllKorisnici();
        const korisnici = response.data;
        console.log("Response : ", response)
        console.log("Korisnici: ", korisnici)
        setUserList(korisnici);
        setShowUserList(!showUserList);
      } catch (error) {
        console.error('Greška prilikom dohvaćanja korisnika', error);
      }
    }
  };

  const handleShowPendingLeaders = async () => {
    if (username === 'Admin') {
      try {
        const response = await getAllKorisniciForApproval();
        const pendingLeaders = response.data;
        setPendingLeaders(pendingLeaders);
        setShowPendingLeadersModal(true);
      } catch (error) {
        console.error('Greška prilikom dohvaćanja voditelja koji čekaju odobrenje', error);
      }
    }
  };

  return (
    <>
      <Appbar setJwtToken={setJwtToken} />
      <Stack
        direction="column"
        justifyContent="flex-start"
        alignItems="flex-start"
        sx={{
          marginTop: "2em",
          marginLeft: "2em"
        }}
      >
        <Typography variant="h5" gutterBottom>
          Dobrodošli {username}!
        </Typography>

        {username === 'Admin' && (
          <>
            <Button onClick={handleToggleUserList}>
              {showUserList ? 'Sakrij listu korisnika' : 'Prikaži listu korisnika'}
            </Button>

            {showUserList && (
              <div>
                <Typography variant="h6" sx={{ marginBottom: '0.5em' }}>
                  Registrirani korisnici:
                </Typography>
                <ul style={{ listStyle: 'none', padding: 0 }}>
                  {userList.map((user) => (
                    <li key={user.id}>
                      <Paper
                        elevation={3}
                        sx={{
                          padding: '0.5em',
                          marginBottom: '0.5em',
                          borderRadius: '8px',
                        }}
                      >
                        <div>
                          <Typography variant="subtitle1">{user.username}  {user.password} {user.razinaPristupa} {user.name} {user.surname} {user.pictureData} {user.bankAccountNumber} {user.email}</Typography>
                        </div>
                      </Paper>
                    </li>
                  ))}
                </ul>
              </div>
            )}
          </>
        )}
      </Stack>
      <Button onClick={handleShowPendingLeaders}>
        Prikaži voditelje koji čekaju odobrenje
      </Button>

      <Modal open={showPendingLeadersModal} onClose={() => setShowPendingLeadersModal(false)}>
        <div>
          <Typography variant="h6">Voditelji koji čekaju odobrenje:</Typography>
          <ul style={{ listStyle: 'none', padding: 0 }}>
            {pendingLeaders.map((leader) => (
              <li key={leader.id}>
                <Paper
                  elevation={3}
                  sx={{
                    padding: '0.5em',
                    marginBottom: '0.5em',
                    borderRadius: '8px',
                  }}
                >
                  <div>
                    <Typography variant="subtitle1">
                      {leader.username} {leader.name} {leader.surname} {/* Dodajte ostale potrebne informacije */}
                    </Typography>
                    {/* <Button onClick={() => handleApproveLeader(leader.id)}>
                      Odobri voditelja
                    </Button> */}
                  </div>
                </Paper>
              </li>
            ))}
          </ul>
        </div>
      </Modal>

      <Stack
        direction="row"
        justifyContent="flex-end"
        alignItems="flex-end"
        sx={{
          position: 'fixed',
          bottom: 0,
          width: '100%',
          marginBottom: '2em',
          padding: '1em',
        }}
      >
        <Grid
          container spacing={3}
          alignItems="center"
          justifyContent={"flex-end"}
        >
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
    </>
  );
}