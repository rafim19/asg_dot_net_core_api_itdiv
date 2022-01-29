using Newtonsoft.Json;
using System;

namespace Jenshin.Impack.API.Model.Request
{
    public class ItemUpdateRequestDTO
    {
        [JsonProperty("ItemId")]
        public string ItemId { get; set; }
        [JsonProperty("ItemName")]
        public string? ItemName { get; set; }
        [JsonProperty("ItemDescription")]
        public string? ItemDescription { get; set; }
        [JsonProperty("ItemPrice")]
        public int? ItemPrice { get; set; }
        [JsonProperty("GenesisCrystalOnly")]
        public bool? GenesisCrystalOnly { get; set; }
        [JsonProperty("Stsrc")]
        public string? Stsrc { get; set; }
    }
}
