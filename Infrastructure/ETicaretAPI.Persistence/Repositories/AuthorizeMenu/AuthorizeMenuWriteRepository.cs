using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class AuthorizeMenuWriteRepository : WriteRepository<AuthorizeMenu>, IAuthorizeMenuWriteRepository
    {
        public AuthorizeMenuWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
