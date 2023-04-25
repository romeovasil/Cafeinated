export interface Login {
  email: string;
  password: string;
}

export interface Session {
  userId: string;
  username: string;
  token: string;
  tokenType: string;
  role: string;
}
