import React, { createContext, useContext, ReactNode } from "react";

interface UserContextProps {
   jwtToken: string | null;
   setJwtToken: (jwtToken: string | null) => void;
   username: string | null;
   setUsername: (username: string | null) => void;
 }
 
 export const UserContext = createContext<UserContextProps | undefined>(undefined);
 
 export const UserProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
   const [jwtToken, setJwtToken] = React.useState<string | null>(null);
   const [username, setUsername] = React.useState<string | null>(null);
   console.log("Username in UserProvider:", username);
 
   return (
     <UserContext.Provider value={{ jwtToken, setJwtToken, username, setUsername }}>
       {children}
     </UserContext.Provider>
   );
 };
 
 export const useUser = () => {
   const context = useContext(UserContext);
   if (!context) {
     throw new Error("useUser must be used within a UserProvider");
   }
   return context;
 };
