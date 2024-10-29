import { Icon } from "@iconify/react/dist/iconify.js";

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
      <Icon
        width="52"
        height="52"
        icon="game-icons:sea-turtle"
        style={{ color: "white" }}
      />
    </header>
  );
};

export default Header;
