import {
  Button,
  FormControl,
  FormHelperText,
  FormLabel,
  Input,
} from "@chakra-ui/react";
import { useContext, useState } from "react";
import { login, LoginRequest } from "../services/accountService";
import { AuthContext } from "../providers/AuthProvider";

const Login = () => {
  const authData = useContext(AuthContext);
  console.log(authData?.token);

  const [email, setEmail] = useState<string | null>(null);
  const [password, setPassword] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (email && password) {
      const request: LoginRequest = { email, password };
      const token = await login(request);

      //localStorage.setItem("token", token);

      console.log(token);
    } else {
      alert("email and password required");
    }
  };

  return (
    <div className="flex flex-col justify-center items-center">
      <form onSubmit={handleSubmit} className="w-1/4">
        <FormControl>
          <FormLabel>Email</FormLabel>
          <Input onChange={(e) => setEmail(e.target.value)} type="email" />
          <FormHelperText>Email</FormHelperText>
        </FormControl>
        <FormControl>
          <FormLabel>Password</FormLabel>
          <Input
            onChange={(e) => setPassword(e.target.value)}
            type="password"
          />
          <FormHelperText>Password</FormHelperText>
        </FormControl>
        <Button type="submit" className="w-full" colorScheme="teal">
          Login
        </Button>
      </form>
    </div>
  );
};

export { Login };
