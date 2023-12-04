﻿using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Domain.Entities
{
    public class Endpoint : BaseEntity
    {
        public string ActionType { get; set; }
        public string HttpType { get; set; }
        public string Definition { get; set; }
        public string Code { get; set; }

        public AuthorizeMenu AuthorizeMenu { get; set; }
        public ICollection<AppRole> Roles { get; set; }

        public Endpoint()
        {
            Roles = new HashSet<AppRole>();
        }
    }
}
