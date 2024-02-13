using ProductAPI.model;

namespace ProductAPI.Repository;

public interface IProductRepository
{
    Product Add(Product product);
    IEnumerable<Product> GetAll();
    Product GetByName(string name);
    Product GetById(int id);
    Product DeleteByID(int id);
    Product Update(int id, Product product);
}
