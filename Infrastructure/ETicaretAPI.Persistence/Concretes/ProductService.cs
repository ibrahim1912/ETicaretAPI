using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
            => new()
            {
                new() {Id = Guid.NewGuid() , Name="Product 1", Price=100, Stock=10},
                new() {Id = Guid.NewGuid() , Name="Product 2", Price=1430, Stock=13},
                new() {Id = Guid.NewGuid() , Name="Product 3", Price=400, Stock=70},
                new() {Id = Guid.NewGuid() , Name="Product 4", Price=700, Stock=16},
                new() {Id = Guid.NewGuid() , Name="Product 5", Price=800, Stock=6},
            };
    }
}
