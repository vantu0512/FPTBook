using FPTBook.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTBook.Models
{
    public class Order
    {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string UId { get; set; }
            public DateTime OrderDate { get; set; }
            public double Total { get; set; }
            public FPTBookUser User { get; set; }
            public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}