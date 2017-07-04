using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using Main.Metadata;

namespace Main.Controllers
{
    public class ProductController : ApiController
    {
        IProductService _productService = null;
        int _perPage = 10;
        public ProductController(IProductService ps)
        {
            _productService = ps;
        }
        // temporararily calling another ctor for DI hiding purposses
        // before nInject would be used
        public ProductController()
            :this(new ProductService())
        {

        }

        //[HttpGet]
        //[HttpHead]
        [AcceptVerbs("GET", "HEAD")]
        [Route("api/Products/page/{page}")]
        [Route("api/Products/")]
        public IEnumerable<ProductInfo> Get(uint page = 1, [FromUri]string lang = "en")
        {
            var _meta = new ProductsMetaData(_productService.Count, (_productService.Count / _perPage) + 1, (int)page);
            _meta.AddMetadataToRequest(Request);

            return _productService.Get(_perPage, (int)(page - 1) * _perPage, lang);
        }

        [HttpGet]
        [Route("api/Products/{id}")]
        public ProductInfo Get(int id, [FromUri]string lang = "en")
        {
            return _productService.GetById(id, lang);
        }

        [AcceptVerbs("GET", "HEAD")]
        [Route("api/products/search")]
        public IEnumerable<ProductInfo> Search([FromUri]string q, [FromUri]uint page = 1, [FromUri]string lang = "en")
        {
            var result = _productService.Find(q, _perPage, (int)(page - 1) * _perPage, lang);
            var dataAmount = _productService.FoundedAmount(q, lang);

            var _meta = new ProductsMetaData((int)dataAmount, ((int)dataAmount / _perPage) + 1, (int)page);
            _meta.AddMetadataToRequest(Request);

            return result;
        }

        [HttpGet]
        [Route("api/Products/top/{amount}")]
        [Route("api/Products/top")]
        public IEnumerable<ProductInfo> GetTop(int amount = 5)
        {
            return (IEnumerable < ProductInfo > )_productService.GetMostBuyableBikes(amount, "en");
        }
    }

}