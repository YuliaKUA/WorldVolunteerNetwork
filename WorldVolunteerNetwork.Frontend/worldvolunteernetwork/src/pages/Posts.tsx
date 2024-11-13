import { useEffect, useState } from "react";
import { PostCard } from "../components/PostCard";
import { getPosts } from "../services/postsService";
import { Post } from "../types/Post";

const Posts = () => {
  const [posts, setPosts] = useState<Post[] | null>([]);

  useEffect(() => {
    const fetch = async () => {
      const postsResponse = await getPosts();
      setPosts(postsResponse);
      console.log(postsResponse);
    };

    fetch();
  }, []);

  return (
    <section className="w-full pt-7 grid gap-5 grid-cols-3">
      {posts?.map((p) => (
        <PostCard key={p.id} post={p} />
      ))}
    </section>
  );
};

export { Posts };
