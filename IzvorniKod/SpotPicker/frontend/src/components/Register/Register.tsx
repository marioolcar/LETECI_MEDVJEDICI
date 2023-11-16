import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  FormControlLabel,
  FormLabel,
  RadioGroup,
  Stack,
  TextField,
  styled,
} from "@mui/material";
import React from "react";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import CloudUploadIcon from "@mui/icons-material/CloudUpload";
import { RegisterUser } from "../../models/Register";
import { Radio } from "@mui/icons-material";
import { register } from "../../services/BackendService";
import Slide from "@mui/material/Slide";
import { TransitionProps } from "@mui/material/transitions";

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
  const { control, handleSubmit } = useForm({
    defaultValues: {
      username: "",
      password: "",
      razinaPristupa: 0,
      name: "",
      surname: "",
      pictureData: null,
      bankAccountNumber: "",
      email: "",
    },
  });

  const onSubmit: SubmitHandler<RegisterUser> = (data) => register(data);
  return (
    <Dialog
      open={!!openRegisterModal}
      TransitionComponent={Transition}
      keepMounted
      onClose={handleClose}
      aria-describedby="alert-dialog-slide-description"
    >
      <DialogTitle>Register</DialogTitle>
      <DialogContent>
        <form onSubmit={handleSubmit(onSubmit)}>
          <Stack
            direction="column"
            justifyContent="center"
            alignItems="center"
            spacing={2}
          >
            <Stack>
              <Controller
                name="username"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    id="username"
                    label="Username"
                    variant="standard"
                  />
                )}
              />
            </Stack>
            <Stack>
              <Controller
                name="password"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    id="password"
                    label="Password"
                    variant="standard"
                    type="password"
                  />
                )}
              />
            </Stack>
            <Stack>
              <Controller
                name="name"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    id="name"
                    label="Name"
                    variant="standard"
                  />
                )}
              />
            </Stack>
            <Stack>
              <Controller
                name="surname"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    id="surname"
                    label="Surname"
                    variant="standard"
                  />
                )}
              />
            </Stack>
            <Stack>
              <Controller
                name="bankAccountNumber"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    id="bankAccountNumber"
                    label="IBAN"
                    variant="standard"
                  />
                )}
              />
            </Stack>
            <Stack>
              <Controller
                name="email"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    id="email"
                    label="Email"
                    type="email"
                    variant="standard"
                  />
                )}
              />
            </Stack>
            <Stack>
              <FormLabel>Razina pristupa</FormLabel>
              <Controller
                name="razinaPristupa"
                control={control}
                render={({ field }) => (
                  <RadioGroup {...field} id="razinaPristupa">
                    <FormControlLabel
                      value="voditelj_parkinga"
                      control={<Radio />}
                      label="Voditelj parkinga"
                    />
                    <FormControlLabel
                      value="klijent"
                      control={<Radio />}
                      label="Klijent"
                    />
                  </RadioGroup>
                )}
              />
            </Stack>
            <Stack>
              <Controller
                name="pictureData"
                control={control}
                render={({ field }) => (
                  <Button
                    {...field}
                    component="label"
                    variant="contained"
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
            <Button onClick={handleClose}>Cancel</Button>
            <Button type="submit" variant="contained">
              Register
            </Button>
          </DialogActions>
        </form>
      </DialogContent>
    </Dialog>
  );
}
