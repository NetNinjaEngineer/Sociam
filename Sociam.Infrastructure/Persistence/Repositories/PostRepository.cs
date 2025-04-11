using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces;

namespace Sociam.Infrastructure.Persistence.Repositories
{
    public sealed class PostRepository(ApplicationDbContext context)
        : GenericRepository<Post>(context), IPostRepository
    {
    }
}
