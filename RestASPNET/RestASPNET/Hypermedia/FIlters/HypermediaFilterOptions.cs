using RestASPNET.Hypermedia.Abstract;

namespace RestASPNET.Hypermedia.FIlters
{
    public class HypermediaFilterOptions
    {
        public List<IResponseEnricher> ResponseEnrichers { get; set; } = new List<IResponseEnricher>();
    }
}
