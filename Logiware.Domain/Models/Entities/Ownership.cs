namespace Logiware.Domain.Models.Entities;

public class Ownership : BaseEntity
{
  

   public int Quantity { get; set; }
   public int SiteId { get; set; }
   public Site Site { get; set; }
   public int ItemId { get; set; }
   public Item Item { get; set; }
   public List<ItemHistory> ItemHistories { get; set; } = new List<ItemHistory>();
   public List<ShipmentItem> ShipmentItems { get; set; }

   
   public Ownership()   
   {
   }

   public Ownership(int quantity, Site site, Item item)
   {
      Quantity = quantity;
      Site = site;
      Item = item;
   }


   public void AdjustQuantity(int adjsutment, string location, Status status)
   {
      var total = Quantity + adjsutment;
      if (total <= 0) throw new ArgumentOutOfRangeException(nameof(adjsutment), "the quantity fall below or equal to zero");
      Quantity = total;

      // if (adjsutment < 0) AddHistory(location, Status.Shipped);
      // else AddHistory(location, Status.Received);

      AddHistory(location, status);
   }

   private void AddHistory(string location, Status status)
   {
      ItemHistories.Add(new ItemHistory(){ Ownership = this, Remarks = location, Status = status});
   }
}