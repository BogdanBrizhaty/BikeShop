using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Main.Filters
{
    public class ProductsMetadataHeaderFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var totalItems = actionExecutedContext.Request.Properties["X-total-items"];
            var totalPages = actionExecutedContext.Request.Properties["X-total-pages"];
            var curPage = actionExecutedContext.Request.Properties["X-current-page"];
            actionExecutedContext.Response.Content.Headers.Add("X-total-items", (string)totalItems);
            actionExecutedContext.Response.Content.Headers.Add("X-total-pages", (string)totalPages);
            actionExecutedContext.Response.Content.Headers.Add("X-current-page", (string)curPage);
        }
    }
}