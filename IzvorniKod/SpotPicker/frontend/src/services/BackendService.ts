import axios from "axios";
import { LoginUser, RegisterUser } from "../models/Register";

const baseURL = "https://spotpicker20231117161516.azurewebsites.net/";


const client = axios.create({
  baseURL: baseURL,
  headers: {'Access-Control-Allow-Origin': '*'}
});

export async function login(data: LoginUser) {
  const response = await client.post(`/Korisnik/Login?username=${data.username}&password=${data.password}`);
  return response;
}

export async function register(data: RegisterUser) {
  const response = await client.post("/Korisnik/Register", { username: data.username, password: data.password, razinaPristupa: data.razinaPristupa, name: data.name, surname: data.surname, pictureData: data.pictureData, bankAccountNumber: data.bankAccountNumber, email: data.email });
  return response;
}
