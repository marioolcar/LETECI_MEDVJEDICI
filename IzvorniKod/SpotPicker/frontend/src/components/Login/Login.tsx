import { TextField } from "@mui/material";
import { useForm, SubmitHandler, Controller } from "react-hook-form";
import { LoginUser } from "../../models/Register";
import { login } from "../../services/BackendService";
import React from "react";

export function Login(): JSX.Element {
  const { control, handleSubmit } = useForm({
    defaultValues: {
      username: "",
      password: "",
    },
  });

  const onSubmit: SubmitHandler<LoginUser> = (data) => login(data);
  return (
    <form onSubmit={handleSubmit(onSubmit)}>
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
      <input type="submit" />
    </form>
  );
}
