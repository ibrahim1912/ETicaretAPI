using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class AuthorizeMenuReadRepository : ReadRepository<AuthorizeMenu>, IAuthorizeMenuReadRepository
    {
        public AuthorizeMenuReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
