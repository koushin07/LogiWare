using System.Security.Cryptography;
using System.Text;
using Logiware.Domain.Enums;

namespace Logiware.Domain.Models.Entities;

public class Personnel : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Site Site { get; set; }
    public int SiteId { get; set; }
    public Role Role { get; set; }
    public string Code { get; set; }

    public List<Shipment> DrivenShipments { get; set; }
    public List<Shipment> AuthorizedShipments { get; set; }

    public Personnel()
    {
        Code = GenerateCode();
    }

    public string FullName()
    {
        return FirstName + LastName;
    }
    
    
    private string GenerateCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        StringBuilder result = new StringBuilder(6);
        
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] buffer = new byte[sizeof(uint)];

            for (int i = 0; i < 6; i++)
            {
                rng.GetBytes(buffer);
                uint num = BitConverter.ToUInt32(buffer, 0);
                result.Append(chars[(int)(num % (uint)chars.Length)]);
            }
        }
        
        return result.ToString();
    }
   
}