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
  KLIJENT
}

export type LoginUser = {
  username: string;
  password: string;
};
