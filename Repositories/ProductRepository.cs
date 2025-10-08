using LAB8_David_Belizario.Data;
using LAB8_David_Belizario.Models;
using LAB8_David_Belizario.Repositories.IRepositories;

namespace LAB8_David_Belizario.Repositories;

public sealed class ProductRepository : ReadRepository<Product>, IProductRepository
{
    public ProductRepository(DbContext context) : base(context)
    {
    }
}
