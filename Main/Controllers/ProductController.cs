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
        AdventureWorksDBEntities _dbe;// = new AdventureWorksDBEntities();
        int _perPage = 10;
        //public ProductController(AdventureWorksDBEntities db)
        //{
        //    _dbe = db;
        //}
        public ProductController()
        {
            _dbe = AdventureWorksDBEntities.Instance;//new AdventureWorksDBEntities();
        }

        [HttpGet]
        [HttpHead]
        [Route("api/Products/page/{page}")]
        [Route("api/Products/")]
        public IEnumerable<ProductInfo> Get(uint page = 1)
        {
            var result = _dbe.Product.Select(p => new ProductInfo
            {
                Id = p.ProductID,
                Name = p.Name,
                Price = p.ListPrice,
                Img = p.ProductProductPhoto.FirstOrDefault().ProductPhoto.ThumbNailPhoto,
                Description = p.ProductModel.ProductModelProductDescriptionCulture.Where(s => s.CultureID == "en").FirstOrDefault().ProductDescription.Description//.ProductDescription.Description
            }).OrderBy(p => p.Id);

            var totalItems = _dbe.Product.Count();
            Request.Properties["X-total-items"] = totalItems.ToString();
            Request.Properties["X-total-pages"] = ((int)(totalItems / _perPage) + 1).ToString();
            Request.Properties["X-current-page"] = page.ToString();

            return ((page == 1) ? result.Take(_perPage) : result.Skip(((int)page - 1) * _perPage).Take(_perPage));
        }

        [HttpGet]
        [Route("api/Products/{id}")]
        public ProductInfo Get(int id, [FromUri]string lang = "en")
        {
            return _dbe.Product
                .Where(p => p.ProductID == id)
                .Select(p => new ProductInfo
                {
                    Id = p.ProductID,
                    Name = p.Name,
                    Description = p.ProductModel.ProductModelProductDescriptionCulture.Where(s => s.CultureID == lang).FirstOrDefault().ProductDescription.Description,
                    Price = p.ListPrice,
                    Img = p.ProductProductPhoto.FirstOrDefault().ProductPhoto.LargePhoto
                }).FirstOrDefault();
        }

        [HttpGet]
        [HttpHead]
        [Route("api/products/search")]
        public IEnumerable<ProductInfo> Search([FromUri]string q, [FromUri]uint page = 1)
        {
            var result = _dbe.Product
                .Where(p => p.Name.ToLower().Contains(q.ToLower()))
                .Select(p => new ProductInfo
                {
                    Id = p.ProductID,
                    Name = p.Name,
                    Price = p.ListPrice,
                    Img = p.ProductProductPhoto.FirstOrDefault().ProductPhoto.ThumbNailPhoto,
                    Description = p.ProductModel.ProductModelProductDescriptionCulture.Where(s => s.CultureID == "en").FirstOrDefault().ProductDescription.Description//.ProductDescription.Description
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
            var allBikeIds = _dbe.Product
                .Where(p => p.ProductSubcategory.ProductCategoryID == 1)
                .Select(p => p.ProductID);

            var mostPopulardBikeIds = _dbe.SalesOrderDetail
                .Where(p => allBikeIds.Contains(p.ProductID))
                .GroupBy(p => p.ProductID)
                .OrderByDescending(p => p.Count())
                .Take(amount)
                .Select(p => p.Key)
                .ToList();

            var result = _dbe.Product
                .Where(p => mostPopulardBikeIds.Contains(p.ProductID))
                .Select(p =>
                    new ProductInfo()
                    {
                        Id = p.ProductID,
                        Name = p.Name,
                        Price = p.ListPrice,
                        Img = p.ProductProductPhoto.FirstOrDefault().ProductPhoto.ThumbNailPhoto,
                        //FullScale = p.ProductProductPhoto.FirstOrDefault().ProductPhoto.LargePhoto,
                        Description = p.ProductModel.ProductModelProductDescriptionCulture.Where(s => s.CultureID == "en").FirstOrDefault().ProductDescription.Description//.ProductDescription.Description
                    }
                ).ToList();
            return result;
        }
    }

}