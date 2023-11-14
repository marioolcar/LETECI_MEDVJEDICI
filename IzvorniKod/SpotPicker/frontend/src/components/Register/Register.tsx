import {
  Button,
  FormControlLabel,
  FormLabel,
  RadioGroup,
  TextField,
  styled,
} from "@mui/material";
import React from "react";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import CloudUploadIcon from "@mui/icons-material/CloudUpload";
import { RegisterUser } from "../../models/Register";
import { Radio } from "@mui/icons-material";
import { register } from "../../services/BackendService";

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

export function Register() {
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
      <Controller
        name="razinaPristupa"
        control={control}
        render={({ field }) => (
          <>
            <FormLabel>Razina pristupa</FormLabel>
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
          </>
        )}
      />
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
      <input type="submit" />
    </form>
  );
}
