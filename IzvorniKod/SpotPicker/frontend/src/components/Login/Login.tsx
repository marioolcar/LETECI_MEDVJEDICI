import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  FormControl,
  Stack,
  TextField,
} from "@mui/material";
import { useForm, SubmitHandler, Controller } from "react-hook-form";
import { LoginUser } from "../../models/Register";
import { login } from "../../services/BackendService";
import React from "react";
import Slide from "@mui/material/Slide";
import { TransitionProps } from "@mui/material/transitions";

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
}
export function Login({
  openLoginModal,
  handleClose,
}: LoginProps): JSX.Element {
  const { control, handleSubmit } = useForm({
    defaultValues: {
      username: "",
      password: "",
    },
  });

  const onSubmit: SubmitHandler<LoginUser> = (data) => login(data);
  return (
    <Dialog
      open={!!openLoginModal}
      TransitionComponent={Transition}
      keepMounted
      onClose={handleClose}
      maxWidth="md"
    >
      <DialogTitle>Login</DialogTitle>
      <DialogContent>
          <form onSubmit={handleSubmit(onSubmit)}>
          <Stack
          direction="column"
          justifyContent="center"
          alignItems="center"
          spacing={2}
          sx={{marginBottom: '1em'}}
        >
            <Stack>
              <Controller
                name="username"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    required
                    autoFocus
                    id="username_login"
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
                    id="password_login"
                    label="Password"
                    variant="standard"
                    type="password"
                  />
                )}
              />
            </Stack>
            </Stack>

              <DialogActions>
                <Button fullWidth type="submit" variant="contained">
                  Login
                </Button>
              </DialogActions>
          </form>
      </DialogContent>
    </Dialog>
  );
}
