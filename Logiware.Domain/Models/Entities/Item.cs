namespace Logiware.Domain.Models.Entities;

public class Item : BaseEntity
{
    public string Name { get; set; }
    public string ItemCode { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }

    public List<Ownership> Ownerships { get; set; } = new List<Ownership>();
   
    public Item()
    {
        ItemCode = GenerateCode();
        CreatedAt = DateTime.Now;
    }

    public Item(string name, string description, string category, int quantity)
    {
        ItemCode = GenerateCode(); 
        Name = name;
        Description = description;
        Category = category;
        CreatedAt = DateTime.Now;
    }

    public void AddOwnerShip(Site site, int quantity)
    {
        Ownerships.Add(
            new Ownership()
            {
                Item = this,
                Site = site,
                Quantity = quantity
            }
            );
    }

    private string GenerateCode()
    {
        var number = new Random().Next(1000, 10000);

        return $"ITM-{number}";
    }
    
   


   
}