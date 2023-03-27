using RestASPNET.Hypermedia;
using RestASPNET.Hypermedia.Abstract;

namespace RestASPNET.Data.VO
{
    public class BookVO : ISupportsHypermedia
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Launch_Date { get; set; }
        public decimal Price { get; set; }
        public List<HypermediaLink> Links { get; set; } = new List<HypermediaLink>();
    }
}
