import { Tab, TabList, Tabs } from "@chakra-ui/react";
import "./App.css";
import Header from "./components/Header";
import { Link, Outlet } from "react-router-dom";

const App = () => {
  return (
    <div>
      <Header />
      <Tabs colorScheme="teal" isFitted>
        <TabList>
          <Tab as={Link} to={"/"}>
            Posts
          </Tab>
          <Tab as={Link} to={"/organizers"}>
            Organizers
          </Tab>
          <Tab as={Link} to={"/news"}>
            News and Update
          </Tab>
        </TabList>
      </Tabs>
      <main className="min-h-screen">
        <Outlet />
      </main>
      <footer />
    </div>
  );
};

export default App;
