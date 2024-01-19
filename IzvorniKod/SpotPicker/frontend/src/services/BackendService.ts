import axios from "axios";
import { LoginUser, RegisterUser, Korisnik } from "../models/Register";
import { toast } from "react-toastify";

const baseURL = "https://spotpicker20231117161516.azurewebsites.net/";

const client = axios.create({
  baseURL: baseURL,
  headers: {
    "Access-Control-Allow-Origin": "*",
    "Authorization": `Bearer ${localStorage.getItem("jwt-token") || ""}`,
  },
});
axios.interceptors.request.use(function (config) {
  const token = localStorage.getItem("jwt-token") || "";
  config.headers.Authorization = token;

  return config;
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
  axios.interceptors.request.use(function (config) {
    const token = localStorage.getItem("jwt-token") || "";
    config.headers.Authorization = token;

    return config;
  });
  return await client.get("/Korisnik/GetAllKorisnikForApproval");
}

export async function updateKorisnik(data: Korisnik) {
  axios.interceptors.request.use(function (config) {
    const token = localStorage.getItem("jwt-token") || "";
    config.headers.Authorization = token;

    return config;
  });
  try {
    const response = await client.post(`/Korisnik/updateKorisnik`, data);

    if (response.data) {
      console.log("Uspjesno si promijenio")
    } else {
      console.error('Nisi promijenio:', response);
    }
  } catch (error) {
    console.error('Error updating user:', error);
    toast.error("Došlo je do pogreške prilikom promjene podataka.");
  }
}

export async function changeBalance(userID: any, amount: number) {
  axios.interceptors.request.use(function (config) {
    const token = localStorage.getItem("jwt-token") || "";
    config.headers.Authorization = token;

    return config;
  });
  const response = await client.post(
    `/Korisnik/ChangeBalance?korisnikID=${userID}&amount=${amount}`,
  );
  console.log("Dodao si novac!!!")
  return response;
}