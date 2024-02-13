namespace ProductAPI.model;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Pris { get; set; }
    public string Description { get; set; } = string.Empty;
}
