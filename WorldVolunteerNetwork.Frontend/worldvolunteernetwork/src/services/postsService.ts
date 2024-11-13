import axios from "axios";
import { Post } from "../types/Post";
import { Envelope } from "../types/Envelope";

const POSTS_URL = "http://localhost:5000/Post"

export const getPosts = async (): Promise<Post[] | null> => {
    const response = await axios.get<Envelope<Post[]>>(POSTS_URL);

    return response.data.result;
  };