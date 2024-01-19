import { Button, Grid, Modal, Paper, Stack, TextField, Typography } from "@mui/material";
import React, { useState } from "react";
import { useUser } from "../../contexts/UserContext";
import { Appbar } from "../../components/Appbar/Appbar";
import 'react-toastify/dist/ReactToastify.css';
import { updateKorisnik, getAllKorisnici, getAllKorisniciForApproval } from "../../services/BackendService";
import { toast } from "react-toastify";

interface MainProps {
  setJwtToken: any;
}

interface Korisnik {
  korisnikID: number;
  username: string;
  password: string;
  razinaPristupa: number;
  name: string;
  surname: string;
  bankAccountNumber: string;
  email: string;
  pictureData: string | null,
  accountEnabled: boolean,
  emailVerified: boolean,
  confirmationCode: string
}

export function Main({ setJwtToken }: MainProps): JSX.Element {
  const { jwtToken, username } = useUser();
  const [walletAmount, setWalletAmount] = useState<number>(0);
  const [totalMoney, setTotalMoney] = useState<number>(0);
  const [userList, setUserList] = useState<Korisnik[]>([]);
  const [showUserList, setShowUserList] = useState(false);
  const [editingUser, setEditingUser] = useState({} as Korisnik | null);
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


  const handleEditUser = (user: Korisnik, field: string, value: string) => {
    setEditingUser({ ...user, [field]: value });
  };

  const handleUpdate = async () => {
    if (editingUser) {
      try {
        await updateKorisnik({
          korisnikID: editingUser.korisnikID,
          username: editingUser.username,
          password: editingUser.password,
          razinaPristupa: editingUser.razinaPristupa,
          name: editingUser.name,
          surname: editingUser.surname,
          bankAccountNumber: editingUser.bankAccountNumber,
          email: editingUser.email,
          pictureData: editingUser.pictureData,
          accountEnabled: editingUser.accountEnabled,
          emailVerified: editingUser.emailVerified,
          confirmationCode: editingUser.confirmationCode
        });
      } catch (error) {
        toast.error('Error updating user');
        console.error('Error updating user:', error);
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

        <Stack
          direction="row"
          justifyContent="flex-start"
          alignItems="flex-start"
          sx={{
            marginTop: "2em",
            marginLeft: "2em"
          }}
        >


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
                      <li key={user.korisnikID}>
                        <Paper
                          elevation={3}
                          sx={{
                            padding: '0.5em',
                            marginBottom: '0.5em',
                            borderRadius: '8px',
                          }}
                        >
                          <Grid container spacing={2}>
                            <Grid item xs={2}>
                              <TextField
                                helperText="Korisničko ime"
                                variant="outlined"
                                defaultValue={user.username || ''}
                                onChange={(e) => handleEditUser(user, 'username', e.target.value)}
                              />
                            </Grid>
                            <Grid item xs={2}>
                              <TextField
                                helperText="Šifra"
                                variant="outlined"
                                defaultValue={user.password || ''}
                                onChange={(e) => handleEditUser(user, 'password', e.target.value)}
                              />
                            </Grid>
                            <Grid item xs={2}>
                              <TextField
                                helperText="Razina pristupa"
                                variant="outlined"
                                type="number"
                                defaultValue={user.razinaPristupa || ''}
                                onChange={(e) => handleEditUser(user, 'razinaPristupa', e.target.value)}
                              />
                            </Grid>
                            <Grid item xs={2}>
                              <TextField
                                helperText="Ime"
                                variant="outlined"
                                defaultValue={user.name || ''}
                                onChange={(e) => handleEditUser(user, 'name', e.target.value)}
                              />
                            </Grid>
                            <Grid item xs={2}>
                              <TextField
                                helperText="Prezime"
                                variant="outlined"
                                defaultValue={user.surname || ''}
                                onChange={(e) => handleEditUser(user, 'surname', e.target.value)}
                              />
                            </Grid>
                            <Grid item xs={2}>
                              <TextField
                                helperText="Broj Računa"
                                variant="outlined"
                                defaultValue={user.bankAccountNumber || ''}
                                onChange={(e) => handleEditUser(user, 'bankAccountNumber', e.target.value)}
                              />
                            </Grid>
                            <Grid item xs={2}>
                              <TextField
                                helperText="email"
                                variant="outlined"
                                defaultValue={user.email || ''}
                                onChange={(e) => handleEditUser(user, 'email', e.target.value)}
                              />
                            </Grid>
                            <Grid item xs={2}>
                              <TextField
                                helperText="pictureData"
                                variant="outlined"
                                defaultValue={user.pictureData || ''}
                                onChange={(e) => handleEditUser(user, 'pictureData', e.target.value)}
                              />
                            </Grid>
                          </Grid>
                        </Paper>
                        <Button onClick={() => handleUpdate()}>Edit</Button>
                      </li>
                    ))}
                  </ul>
                </div>
              )}
            </>
          )}



          {username === 'Admin' && (
            <>
              <Button onClick={handleShowPendingLeaders}>
                Prikaži voditelje koji čekaju odobrenje
              </Button>

              <Modal open={showPendingLeadersModal} onClose={() => setShowPendingLeadersModal(false)}>
                <div style={{ padding: '1em' }}>
                  <Typography variant="h6">Voditelji koji čekaju odobrenje:</Typography>
                  <ul style={{ listStyle: 'none', padding: 0 }}>
                    {pendingLeaders.map((leader) => (
                      <li key={leader.korisnikID}>
                        <Paper
                          elevation={3}
                          sx={{
                            padding: '1em',
                            marginBottom: '1em',
                            borderRadius: '8px',
                          }}
                        >
                          <Grid container spacing={2}>
                            <Grid item xs={3}>
                              <TextField
                                fullWidth
                                helperText="Korisničko ime"
                                variant="outlined"
                                defaultValue={leader.username || ''}
                                onChange={(e) => handleEditUser(leader, 'username', e.target.value)}
                              />
                            </Grid>
                            <Grid item xs={3}>
                              <TextField
                                fullWidth
                                helperText="Šifra"
                                variant="outlined"
                                defaultValue={leader.password || ''}
                                onChange={(e) => handleEditUser(leader, 'password', e.target.value)}
                              />
                            </Grid>
                            <Grid item xs={3}>
                              <TextField
                                fullWidth
                                helperText="Razina pristupa"
                                variant="outlined"
                                type="number"
                                defaultValue={leader.razinaPristupa || ''}
                                onChange={(e) => handleEditUser(leader, 'razinaPristupa', e.target.value)}
                              />
                            </Grid>
                            <Grid item xs={3}>
                              <Button>
                                Odobri voditelja
                              </Button>
                            </Grid>
                          </Grid>
                        </Paper>
                      </li>
                    ))}
                  </ul>
                </div>
              </Modal>
            </>
          )}
        </Stack>
      </Stack>
      <Stack
        direction="row"
        justifyContent="flex-end"
        alignItems="flex-end"
        sx={{
          position: 'relative',
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