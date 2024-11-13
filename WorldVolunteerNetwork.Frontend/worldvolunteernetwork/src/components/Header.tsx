import { Icon } from "@iconify/react/dist/iconify.js";
import { Link, redirect } from "react-router-dom";

const Header = () => {
  return (
    <header className="w-full py-3 flex flex-row justify-between items-center bg-teal-800 shadow-xl">
      <Icon
        width="52"
        height="52"
        icon="game-icons:sea-turtle"
        style={{ color: "white" }}
      />
      <span className="text-3xl text-white">World volunteer network</span>
      <Link to={"/login"}>
        <Icon
          width="52"
          height="52"
          icon="circum:login"
          style={{ color: "white" }}
          onClick={() => redirect("/login")}
        />
      </Link>
    </header>
  );
};

export default Header;
