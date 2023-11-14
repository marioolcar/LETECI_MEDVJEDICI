import axios from "axios";
import { LoginUser, RegisterUser } from "../models/Register";

const client = axios.create({
  baseURL: process.env.REACT_APP_BASE_URL,
});

export async function login(data: LoginUser) {
  const response = await client.get("/Korisnik/Login/Login", { data });
  return response;
}

export async function register(data: RegisterUser) {
  const response = await client.get("/Korisnik/Register/Register", { data });
  return response;
}
