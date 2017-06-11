using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;

namespace Main.Controllers
{
    public class ProductController : ApiController
    {
        AdventureWorksDBEntities dbe = new AdventureWorksDBEntities();
        // GET api/<controller>
        [HttpGet]
        [Route("api/Product/page/{page}")]
        //[Route("api/Product/page")]
        [Route("api/Product/")]
        public IEnumerable<string> Get(int page = 1)
        {
            //var 
            //return 
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Product/{id}")]
        public ProductInfo Get(int id, [FromUri]string lang = "en")
        {
            return dbe.Product
                .Where(p => p.ProductModel.ProductModelProductDescriptionCulture.FirstOrDefault().CultureID == lang)
                .Select(p => new ProductInfo
                {
                    Id = p.ProductID,
                    Name = p.Name,
                    Description = p.ProductModel.ProductModelProductDescriptionCulture.FirstOrDefault().ProductDescription.Description,
                    Price = p.ListPrice,
                    FullScale = p.ProductProductPhoto.FirstOrDefault().ProductPhoto.LargePhoto
                }).FirstOrDefault();
        }
        [HttpGet]
        [Route("api/product/search")]
        public string Search([FromUri]string q)
        {
            return q;
        }
        [HttpGet]
        [Route("api/Product/top/{amount}")]
        [Route("api/Product/top")]
        public IEnumerable<ProductInfo> GetTop(int amount = 5)
        {
            var allBikeIds = dbe.Product
                .Where(p => p.ProductSubcategory.ProductCategoryID == 1)
                .Select(p => p.ProductID);

            var mostPopulardBikeIds = dbe.SalesOrderDetail
                .Where(p => allBikeIds.Contains(p.ProductID))
                .GroupBy(p => p.ProductID)
                .OrderByDescending(p => p.Count())
                .Take(amount)
                .Select(p => p.Key)
                .ToList();

            var result = dbe.Product
                .Where(p => mostPopulardBikeIds.Contains(p.ProductID))
                .Select(p =>
                    new ProductInfo()
                    {
                        Id = p.ProductID,
                        Name = p.Name,
                        Price = p.ListPrice,
                        Thumbnail = p.ProductProductPhoto.FirstOrDefault().ProductPhoto.ThumbNailPhoto,
                        FullScale = p.ProductProductPhoto.FirstOrDefault().ProductPhoto.LargePhoto
                    }
                ).ToList();
            return result;
        }
    }

}