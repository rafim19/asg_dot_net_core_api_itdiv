using Binus.WS.Pattern.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jenshin.Impack.API.Model
{
    [DatabaseName("JenshinDB")]
    [Table("TrUserPurchase")]
    public class TrUserPurchase : BaseModel
    {
        [Column("PurchaseID")]
        [Key]
        public Guid PurchaseID { get; set; }
        [Column("UserID")]
        public Guid UserID { get; set; }
        [Column("ItemID")]
        public Guid ItemID { get; set; }
        [Column("PurchaseAmount")]
        public int PurchaseAmount { get; set; }
    }
}
