using Newtonsoft.Json;

namespace Jenshin.Impack.API.Model.Request
{
    public class AddItemRequestDTO
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int? ItemPrice { get; set; }
        public bool? GenesisCrystalOnly { get; set; }
    }
}
