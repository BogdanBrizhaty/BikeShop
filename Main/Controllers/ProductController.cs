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
        int _perPage = 10;

        [HttpGet]
        [HttpHead]
        [Route("api/Products/page/{page}")]
        [Route("api/Products/")]
        public IEnumerable<ProductInfo> Get(uint page = 1)
        {
            var result = dbe.Product.Select(p => new ProductInfo
            {
                Id = p.ProductID,
                Name = p.Name,
                Price = p.ListPrice,
                Thumbnail = p.ProductProductPhoto.FirstOrDefault().ProductPhoto.ThumbNailPhoto
            }).OrderBy(p => p.Id);

            var totalItems = dbe.Product.Count();
            Request.Properties["X-total-items"] = totalItems.ToString();
            Request.Properties["X-total-pages"] = ((int)(totalItems / _perPage) + 1).ToString();
            Request.Properties["X-current-page"] = page.ToString();

            return ((page == 1) ? result.Take(_perPage) : result.Skip(((int)page - 1) * _perPage).Take(_perPage));
        }

        [HttpGet]
        [Route("api/Products/{id}")]
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
        [HttpHead]
        [Route("api/products/search")]
        public IEnumerable<ProductInfo> Search([FromUri]string q, [FromUri]uint page = 1)
        {
            var result = dbe.Product
                .Where(p => p.Name.ToLower().Contains(q.ToLower()))
                .Select(p => new ProductInfo
                {
                    Id = p.ProductID,
                    Name = p.Name,
                    Price = p.ListPrice,
                    Thumbnail = p.ProductProductPhoto.FirstOrDefault().ProductPhoto.ThumbNailPhoto
                }).OrderBy(p => p.Id);

            var totalItems = result.Count();
            Request.Properties["X-total-items"] = totalItems.ToString();
            Request.Properties["X-total-pages"] = ((int)(totalItems / _perPage) + 1).ToString();
            Request.Properties["X-current-page"] = page.ToString();

            return (page == 1) ? result.Take(_perPage) : result.Skip(((int)page - 1) * _perPage).Take(_perPage);
        }

        [HttpGet]
        [Route("api/Products/top/{amount}")]
        [Route("api/Products/top")]
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