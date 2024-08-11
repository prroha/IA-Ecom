using System.ComponentModel.DataAnnotations;

namespace IA_Ecom.Models;

public class BaseModel
{
    [Key]
    public int Id { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public BaseModel()
    {
        CreatedDate = DateTime.UtcNow;
    }
}