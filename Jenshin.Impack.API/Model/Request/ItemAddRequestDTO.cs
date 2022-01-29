using Newtonsoft.Json;

namespace Jenshin.Impack.API.Model.Request
{
    public class ItemAddRequestDTO
    {
        [JsonProperty("ItemName")]
        public string ItemName { get; set; }
        [JsonProperty("ItemDescription")]
        public string ItemDescription { get; set; }
        [JsonProperty("ItemPrice")]
        public int ItemPrice { get; set; }
        [JsonProperty("GenesisCrystalOnly")]
        public bool GenesisCrystalOnly { get; set; }
    }
}
