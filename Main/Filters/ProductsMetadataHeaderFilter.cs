using Model;
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
            if (!actionExecutedContext.Request.Properties.ContainsKey("X-total-items"))
                return;

            var totalItems = actionExecutedContext.Request.Properties["X-total-items"];
            var totalPages = actionExecutedContext.Request.Properties["X-total-pages"];
            var curPage = actionExecutedContext.Request.Properties["X-current-page"];

            actionExecutedContext.Response.Content.Headers.Add("X-total-items", totalItems.ToString());
            actionExecutedContext.Response.Content.Headers.Add("X-total-pages", totalPages.ToString());
            actionExecutedContext.Response.Content.Headers.Add("X-current-page", curPage.ToString());
        }
    }
}