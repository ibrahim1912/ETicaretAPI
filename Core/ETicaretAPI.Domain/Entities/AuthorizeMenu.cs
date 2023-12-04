using ETicaretAPI.Domain.Entities.Common;

namespace ETicaretAPI.Domain.Entities
{
    public class AuthorizeMenu : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
