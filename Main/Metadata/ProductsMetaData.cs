using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Main.Metadata
{
    public class ProductsMetaData : BaseMetadata<int>
    {
        public ProductsMetaData(int items, int pages, int currentPage)
        {
            _metadata = new Dictionary<string, int>()
            {
                { "X-total-items", items },
                { "X-total-pages", pages },
                { "X-current-page", currentPage }
            };
        }
    }
}