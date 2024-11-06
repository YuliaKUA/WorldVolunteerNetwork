import axios from "axios";
import { Post } from "../pages/Posts";
import { Envelope } from "../types/Envelope";

const POSTS_URL = "http://localhost:5270/Post"

export const getPosts = async (): Promise<Post[] | null> => {
    const response = await axios.get<Envelope<Post[]>>(POSTS_URL);

    return response.data.result;
  };