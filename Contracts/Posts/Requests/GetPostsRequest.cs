using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Posts.Requests
{
    public record GetPostsRequest(
        string? Name,
        string? Description,
        string? PostStatus,
        int Page = 1,
        int Size = 10);
}
