using ProductAPI.model;

namespace ProductAPI.Repository;

public class ProductInMemoryDb
{
    List<Product> _products = new List<Product>();
    int _lastId = 0;
    
    public Product Add(Product product)
    {
        _lastId++;
        product.Id = _lastId;
        _products.Add(product);

        return product;
    }
}
