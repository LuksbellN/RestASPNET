using RestASPNET.Hypermedia;
using RestASPNET.Hypermedia.Abstract;
using System.Text.Json.Serialization;

namespace RestASPNET.Data.VO
{
    public class PersonVO : ISupportsHypermedia
    {
        [JsonPropertyName ("code")]
        public int Id { get; set; }
        [JsonPropertyName ("name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        [JsonPropertyName("sex")]
        public string Gender { get; set; }
        public bool Enabled { get; set; }
        public List<HypermediaLink> Links { get; set; } = new List<HypermediaLink>();  
    }
}
