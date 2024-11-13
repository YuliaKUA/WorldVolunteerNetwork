import axios from "axios";

const ACCOUNT_URL = "http://localhost:5000/account";

export type LoginRequest = {
  email: string; 
  password: string;
}

export const login = async (request: LoginRequest): Promise<string> => {
    const response = await axios.post(ACCOUNT_URL + "/login", request);
    const token = response.data;
    return token;
  };