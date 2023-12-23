﻿using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using P = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFilE.GetProductImage
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration _configuration;

        public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        public async Task<List<GetProductImagesQueryResponse>?> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));


            return product?.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
            {
                Showcase = p.Showcase,
                Path = $"{_configuration["BaseStorageUrl:CloudinaryUrl"]}/{p.Path}{Path.GetExtension(p.Path)}",
                FileName = p.FileName,
                Id = p.Id
            }).ToList();
        }
    }
}
