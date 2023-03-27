using Microsoft.AspNetCore.Mvc;
using RestASPNET.Data.VO;
using RestASPNET.Hypermedia.Constants;
using System.Text;

namespace RestASPNET.Hypermedia.Enricher
{
    public class BookEnricher : ContentResponseEnricher<BookVO>
    {
        private readonly object _lock = new object();
        protected override Task EnricheModel(BookVO content, IUrlHelper urlHelper)
        {
            var path = "v1/api/book";
            string link = GetLink(content.Id, urlHelper, path);

            content.Links.Add(new HypermediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HypermediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HypermediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });

            content.Links.Add(new HypermediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });
            return null;
        }

        private string GetLink(int id, IUrlHelper urlHelper, string path)
        {
            lock( _lock )
            {
                var url = new
                {
                    controller = path,
                    id = id
                };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            }
        }
    }
}
