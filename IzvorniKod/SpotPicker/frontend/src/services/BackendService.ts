import axios from "axios";
import { LoginUser, RegisterUser } from "../models/Register";

const baseURL = "http://localhost:3000";


const client = axios.create({
  baseURL: baseURL,
  headers: {'Access-Control-Allow-Origin': '*'}
});

export async function login(data: LoginUser) {
  const response = await client.post("/Korisnik/Login", { data });
  return response;
}

export async function register(data: RegisterUser) {
  const response = await client.post("/Korisnik/Register", { data });
  return response;
}
