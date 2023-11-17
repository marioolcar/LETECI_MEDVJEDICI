import {
  Alert,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  FormControlLabel,
  FormLabel,
  Radio,
  RadioGroup,
  Snackbar,
  Stack,
  TextField,
  styled,
} from "@mui/material";
import React, { ChangeEvent, useState } from "react";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import CloudUploadIcon from "@mui/icons-material/CloudUpload";
import { RazinaPristupa, RegisterUser } from "../../models/Register";
import { register } from "../../services/BackendService";
import Slide from "@mui/material/Slide";
import { TransitionProps } from "@mui/material/transitions";
import { useMutation } from "@tanstack/react-query";

const VisuallyHiddenInput = styled("input")({
  clip: "rect(0 0 0 0)",
  clipPath: "inset(50%)",
  height: 1,
  overflow: "hidden",
  position: "absolute",
  bottom: 0,
  left: 0,
  whiteSpace: "nowrap",
  width: 1,
});

const Transition = React.forwardRef(function Transition(
  props: TransitionProps & {
    children: React.ReactElement<any, any>;
  },
  ref: React.Ref<unknown>,
) {
  return <Slide direction="up" ref={ref} {...props} />;
});

interface RegisterProps {
  openRegisterModal?: boolean;
  handleClose?: () => void;
}
export function Register({ openRegisterModal, handleClose }: RegisterProps) {
  const [openSnackbar, setOpenSnackbar] = useState<boolean>(false);
  const { control, handleSubmit, reset } = useForm({
    defaultValues: {
      username: "",
      password: "",
      razinaPristupa: RazinaPristupa.VODITELJ_PARKINGA,
      name: "",
      surname: "",
      pictureData: null,
      bankAccountNumber: "",
      email: "",
    },
  });

  const { mutate } = useMutation({
    mutationFn: (data: RegisterUser) => register(data),
    onSuccess: () => {
      setOpenSnackbar(true);
      reset();
    },
  });
  const onSubmit: SubmitHandler<RegisterUser> = (data) => mutate(data);
  return (
    <Dialog
      open={!!openRegisterModal}
      TransitionComponent={Transition}
      keepMounted
      onClose={handleClose}
      maxWidth="sm"
      fullWidth
    >
      <DialogTitle>Register</DialogTitle>
      <DialogContent sx={{width: '100%'}}>
        <form onSubmit={handleSubmit(onSubmit)}>
          <Stack
            direction="column"
            justifyContent="center"
            alignItems="center"
            spacing={2}
            sx={{width: '100%', marginBottom: '1em'}}
          >
            <Stack sx={{width: '100%'}}>
              <Controller
                name="username"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    id="username"
                    label="Korisničko ime"
                    variant="standard"
                    fullWidth
                  />
                )}
              />
            </Stack>
            <Stack sx={{width: '100%'}}>
              <Controller
                name="password"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    fullWidth
                    id="password"
                    label="Lozinka"
                    variant="standard"
                    type="password"
                  />
                )}
              />
            </Stack>
            <Stack sx={{width: '100%'}}>
              <Controller
                name="name"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    fullWidth
                    id="name"
                    label="Ime"
                    variant="standard"
                  />
                )}
              />
            </Stack>
            <Stack sx={{width: '100%'}}>
              <Controller
                name="surname"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    fullWidth
                    id="surname"
                    label="Prezime"
                    variant="standard"
                  />
                )}
              />
            </Stack>
            <Stack sx={{width: '100%'}}>
              <Controller
                name="bankAccountNumber"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    fullWidth
                    id="bankAccountNumber"
                    label="IBAN"
                    variant="standard"
                  />
                )}
              />
            </Stack>
            <Stack sx={{width: '100%'}}>
              <Controller
                name="email"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    fullWidth
                    id="email"
                    label="Email"
                    type="email"
                    variant="standard"
                  />
                )}
              />
            </Stack>
            <Stack direction="column" alignItems="flex-start" justifyContent="flex-start" sx={{width: '100%'}}>
              <FormLabel>Razina pristupa</FormLabel>
              <Controller
                name="razinaPristupa"
                control={control}
                render={({ field }) => (
                  <RadioGroup {...field} id="razinaPristupa">
                    <FormControlLabel
                      value={RazinaPristupa.VODITELJ_PARKINGA}
                      control={<Radio />}
                      label="Voditelj parkinga"
                    />
                    <FormControlLabel
                      value={RazinaPristupa.KLIJENT}
                      control={<Radio />}
                      label="Klijent"
                    />
                  </RadioGroup>
                )}
              />
            </Stack>
            <Stack sx={{width: '100%'}}>
              <Controller
                name="pictureData"
                control={control}
                render={({ field }) => (
                  <Button
                    {...field}
                    component="label"
                    variant="outlined"
                    startIcon={<CloudUploadIcon />}
                  >
                    Upload file
                    <VisuallyHiddenInput type="file" />
                  </Button>
                )}
              />
            </Stack>
          </Stack>
          <DialogActions>
            <Button type="submit" variant="contained">
              Register
            </Button>
          </DialogActions>
        </form>
      </DialogContent>
      <Snackbar open={openSnackbar} autoHideDuration={6000} onClose={() => setOpenSnackbar(false)}>
        <Alert severity="success" sx={{ width: '100%' }} onClose={() => setOpenSnackbar(false)}>
          Uspjeli ste registrirati se!
        </Alert>
      </Snackbar>
    </Dialog>
  );
}
