export interface Register {
  korisnikID: number;
  username: string;
  password: string;
  razinaPristupa: number;
  name: string;
  surname: string;
  pictureData: string;
  bankAccountNumber: string;
  email: string;
  accountEnabled: boolean;
}

export type LoginUser = {
  username: string;
  password: string;
};
