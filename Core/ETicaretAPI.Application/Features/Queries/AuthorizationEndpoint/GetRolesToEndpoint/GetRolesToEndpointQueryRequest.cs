﻿using MediatR;

namespace ETicaretAPI.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint
{
    public class GetRolesToEndpointQueryRequest : IRequest<GetRolesToEndpointQueryResponse>
    {
        public string Code { get; set; }
        public string AuthorizeMenu { get; set; }
    }
}