import { createBrowserRouter } from "react-router-dom";
import App from "./App";
import { Posts } from "./pages/Posts";
import { Login } from "./pages/Login";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: "/",
        element: <Posts />,
      },
      {
        path: "organizers",
        element: <div>Organizers!</div>,
      },
      {
        path: "news",
        element: <div>News!</div>,
      },
      {
        path: "login",
        element: <Login />,
      },
    ],
    errorElement: <div>Error 404 Not found</div>,
  },
]);
