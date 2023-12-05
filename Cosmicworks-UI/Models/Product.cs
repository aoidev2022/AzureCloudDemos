namespace Cosmicworks_UI.Models;


public class Product
{
    public Product(string id, string sku, string name, string description, decimal price)
    {
        this.id = id;
        this.sku = sku;
        this.name = name;
        this.description = description;
        this.price = price;
    }

    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public Category category { get; set; }
    public string sku { get; set; }
    public string[] tags { get; set; }
    public decimal cost { get; set; }
    public decimal price { get; set; }
    public string type { get; set; }
}

public class Category
{
    public string name { get; set; }
    public Subcategory subCategory { get; set; }
}

public class Subcategory
{
    public string name { get; set; }
}
