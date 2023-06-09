﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestASPNET.Hypermedia.FIlters
{
    public class HypermediaFilter : ResultFilterAttribute
    {
        private readonly HypermediaFilterOptions _hypermediaFilterOptions;

        public HypermediaFilter(HypermediaFilterOptions hypermediaFilterOptions)
        {
            _hypermediaFilterOptions = hypermediaFilterOptions;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnrichResult(ResultExecutingContext context)
        {
            if(context.Result is OkObjectResult okObjectResult)
            {
                var enricher = _hypermediaFilterOptions
                    .ResponseEnrichers
                    .FirstOrDefault(x => x.CanEnrich(context));

                if(enricher != null)
                {
                    Task.FromResult(enricher.Enrich(context));
                }
            }
        }
    }
}
