import { createContext, useState } from "react";

export const AuthContext = createContext<AuthContextType | null>(null);

type AuthContextType = {
  token: string | null;
};

export const AuthProvider = ({ children }: any) => {
  const [authData, setAuthData] = useState<AuthContextType | null>(null);

  return (
    <AuthContext.Provider value={authData}>{children}</AuthContext.Provider>
  );
};
