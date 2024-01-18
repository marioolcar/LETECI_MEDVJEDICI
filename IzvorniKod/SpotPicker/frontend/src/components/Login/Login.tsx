import {
  Alert,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Stack,
  TextField,
} from "@mui/material";
import { useForm, SubmitHandler, Controller } from "react-hook-form";
import { LoginUser } from "../../models/Register";
import { login } from "../../services/BackendService";
import React, { useState, useContext } from "react";
import Slide from "@mui/material/Slide";
import { TransitionProps } from "@mui/material/transitions";
import { useMutation } from "@tanstack/react-query";
import { useUser } from "../../contexts/UserContext";

const Transition = React.forwardRef(function Transition(
  props: TransitionProps & {
    children: React.ReactElement<any, any>;
  },
  ref: React.Ref<unknown>,
) {
  return <Slide direction="up" ref={ref} {...props} />;
});

interface LoginProps {
  openLoginModal?: boolean;
  handleClose?: () => void;
  setJwtToken?: any;

}
export function Login({
  openLoginModal,
  handleClose,
  setJwtToken,
}: LoginProps): JSX.Element {
  const { control, handleSubmit, getValues } = useForm({
    defaultValues: {
      username: "",
      password: "",
    },
  });
  const { setUsername } = useUser();
  const [alertError, setAlertError] = useState<boolean>(false);
  const { mutate } = useMutation({
    mutationFn: (data: LoginUser) => login(data),
    onSuccess: (data) => {
      if (data.data) {
        localStorage.setItem("jwt-token", data.data);
        setJwtToken(data.data);
        const enteredUsername = getValues("username");
        setUsername(enteredUsername);
      } else {
        setAlertError(true);
      }
    },
  });
  const onSubmit: SubmitHandler<LoginUser> = (data) => mutate(data);
  return (
    <Dialog
      open={!!openLoginModal}
      TransitionComponent={Transition}
      keepMounted
      onClose={handleClose}
      fullWidth
      maxWidth="sm"
    >
      <DialogTitle>Login</DialogTitle>
      <DialogContent sx={{ width: "100%" }}>
        <form onSubmit={handleSubmit(onSubmit)}>
          <Stack
            direction="column"
            justifyContent="center"
            alignItems="center"
            spacing={2}
          >
            <Stack sx={{ width: "100%" }}>
              <Controller
                name="username"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    autoFocus
                    fullWidth
                    id="username_login"
                    label="Username"
                    variant="standard"
                  />
                )}
              />
            </Stack>
            <Stack sx={{ width: "100%" }}>
              <Controller
                name="password"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    fullWidth
                    id="password_login"
                    label="Password"
                    variant="standard"
                    type="password"
                  />
                )}
              />
            </Stack>
            <Stack sx={{ width: "100%" }}>
              <DialogActions>
                <Stack
                  direction="column"
                  justifyContent="flex-end"
                  alignItems="flex-end"
                  spacing={2}
                  sx={{ marginBottom: "1em" }}
                >
                  <Stack>
                    <Button
                      fullWidth
                      type="submit"
                      variant="contained"
                    >
                      Login
                    </Button>
                  </Stack>
                  <Stack>
                    {alertError && (
                      <Alert severity="error">
                        Wrong username or password.
                      </Alert>
                    )}
                  </Stack>
                </Stack>
              </DialogActions>
            </Stack>
          </Stack>
        </form>
      </DialogContent>
    </Dialog>
  );
}