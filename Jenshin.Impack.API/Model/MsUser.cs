using Binus.WS.Pattern.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jenshin.Impack.API.Model
{
    [DatabaseName("JenshinDB")]
    [Table("MsUser")]
    public class MsUser : BaseModel
    {
        [Key]
        [Column("UserID")]
        public Guid UserID { get; set; }
        [Column("UserName")]
        public string? UserName { get; set; }
        [Column("UserAdventureRank")]
        public int UserAdventureRank { get; set; }
        [Column("UserEmail")]
        public string? UserEmail { get; set; }
        [Column("UserSignature")]
        public string UserSignature { get; set; }
    }
}
