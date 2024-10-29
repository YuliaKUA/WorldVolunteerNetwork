import "./App.css";
import Header from "./components/Header";
import { Posts } from "./pages/Posts";

function App() {
  return (
    <div>
      <Header />
      <main>
        <Posts />
      </main>
      <footer></footer>
    </div>
  );
}

export default App;
