import axios from "axios";
import { LoginUser } from "../models/Register";

const client = axios.create({
  baseURL: process.env.REACT_APP_BASE_URL,
});

export async function login(data: LoginUser) {
  const response = await client.get("/Korisnik/Login/Login", { data });
  return response;
}
