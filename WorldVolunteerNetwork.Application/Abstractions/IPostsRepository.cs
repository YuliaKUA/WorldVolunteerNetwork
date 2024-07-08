using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Application.Abstractions
{
    public interface IPostsRepository
    {
        Task<Result<Guid, Error>> Add(Post post, CancellationToken ct);
    }
}
