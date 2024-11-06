import {
  Button,
  Card,
  CardBody,
  CardFooter,
  Stack,
  Image,
  Heading,
  Text,
} from "@chakra-ui/react";
import { Post } from "../pages/Posts";

type Props = {
  post: Post;
};

const PostCard = ({ post }: Props) => {
  return (
    <Card
      direction={{ base: "column", sm: "row" }}
      maxW={{ base: "100%" }}
      overflow="hidden"
      variant="outline"
    >
      <Image
        objectFit="cover"
        maxW={{ base: "100%", sm: "200px" }}
        src="https://storge.pic2.me/upload/219/5fcd32448cfd64.00573968.jpg"
        alt="Caffe Latte"
      />

      <Stack>
        <CardBody>
          <Heading size="md">{post.name}</Heading>

          <Text py="2">{post.description}</Text>
        </CardBody>

        <CardFooter>
          <Button variant="solid" colorScheme="blue">
            Respond
          </Button>
        </CardFooter>
      </Stack>
    </Card>
  );
};

export { PostCard };
