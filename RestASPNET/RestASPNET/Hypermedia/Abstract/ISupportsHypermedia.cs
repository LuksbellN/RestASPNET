namespace RestASPNET.Hypermedia.Abstract
{
    public interface ISupportsHypermedia
    {
        List<HypermediaLink> Links { get; set; }
    }
}
