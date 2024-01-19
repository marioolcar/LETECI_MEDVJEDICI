import axios from "axios";
import { LoginUser, RegisterUser, UpdateUser } from "../models/Register";

const baseURL = "http://localhost:3001";

const client = axios.create({
  baseURL: baseURL,
  headers: {
    "Access-Control-Allow-Origin": "*",
    "Authorization": `Bearer ${localStorage.getItem("jwt-token") || ""}`,
  },
});

export async function login(data: LoginUser) {
  const response = await client.post(
    `/Korisnik/Login?username=${data.username}&password=${data.password}`,
  );
  return response;
}

export async function register(data: RegisterUser) {
  const response = await client.post("/Korisnik/Register", {
    username: data.username,
    password: data.password,
    razinaPristupa: Number(data.razinaPristupa),
    name: data.name,
    surname: data.surname,
    pictureData: null,
    bankAccountNumber: data.bankAccountNumber,
    email: data.email,
  });
  return response;
}

export async function getAllKorisnici() {
  return await client.get("/Korisnik/GetAllKorisnik");
}

export async function getAllKorisniciForApproval() {
  return await client.get("/Korisnik/GetAllKorisnikForApproval");
}

export async function updateKorisnik(data: UpdateUser) {
  try {
    const response = await client.post(`/Korisnik/updateKorisnik`, data);

    if (response.data) {
      console.log("Uspjesno si promijenio")
    } else {
      console.error('Nisi promijenio:', response);
    }
  } catch (error) {
    console.error('Error updating user:', error);
  }
}