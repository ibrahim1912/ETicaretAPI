﻿using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        readonly ILogger<GetAllProductQueryHandler> _logger;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _logger = logger;
        }


        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all products ");

            //global exception test için 
            // throw new Exception("Hata alındı!!!!");


            var totalProductCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size)
                .Include(p => p.ProductImageFiles)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Stock,
                    p.Price,
                    p.CreatedDate,
                    p.UpdatedDate,
                    p.ProductImageFiles

                }).ToList();



            return new()
            {
                Products = products,
                TotalProductCount = totalProductCount
            };




        }
    }
}
