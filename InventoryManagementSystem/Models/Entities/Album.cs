using Newtonsoft.Json;

namespace InventoryManagementSystem.Models.Entities
{
    public class Album
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty; // Initialize to avoid null warnings

        [JsonProperty("userId")]
        public int UserId { get; set; }
    }
}