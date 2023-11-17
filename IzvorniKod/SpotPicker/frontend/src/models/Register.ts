export interface RegisterUser {
  username: string;
  password: string;
  razinaPristupa: number;
  name: string;
  surname: string;
  pictureData: string | null;
  bankAccountNumber: string;
  email: string;
}

export type LoginUser = {
  username: string;
  password: string;
};
