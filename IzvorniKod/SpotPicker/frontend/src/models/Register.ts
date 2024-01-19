export interface RegisterUser {
  username: string;
  password: string;
  razinaPristupa: RazinaPristupa;
  name: string;
  surname: string;
  pictureData: string | null;
  bankAccountNumber: string;
  email: string;
}

export enum RazinaPristupa {
  VODITELJ_PARKINGA,
  KLIJENT,
}

export type LoginUser = {
  username: string;
  password: string;
};


export interface Korisnik {
  korisnikID: number;
  username: string;
  password: string;
  razinaPristupa: number;
  name: string;
  surname: string;
  bankAccountNumber: string;
  email: string;
  pictureData: string | null,
  accountEnabled: boolean,
  emailVerified: boolean,
  confirmationCode: string
}