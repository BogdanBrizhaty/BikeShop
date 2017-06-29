using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Some simple business logic for products purposses
    /// </summary>
    public class ProductService : IProductService
    {
        AdventureWorksDBEntities _db;
        public ProductService()
        {
            _db = new AdventureWorksDBEntities();
        }
        public long Count { get { return _db.Product.Count(); } }


        // IProductService
        #region IProductService implementation

        /// <summary>
        /// Returns ALL of the products
        /// </summary>
        /// <param name="lang">language for product description</param>
        /// <returns></returns>
        public IEnumerable<ProductInfo> GetAll(string lang)
        {
            return _db.Product
                .Select(p => new ProductInfo(p, p.ProductProductPhoto.FirstOrDefault().ProductPhoto.ThumbNailPhoto, lang));
        }

        /// <summary>
        /// Returns amounts of products
        /// </summary>
        /// <param name="portion">A number of products to select</param>
        /// <param name="startAt">Position. Number of elements to skip</param>
        /// <param name="lang">language for product description</param>
        public IEnumerable<ProductInfo> Get(int portion, int startAt, string lang)
        {
            // need to check if portion is bigger than data amount, if startAt is bigger than data amount

            return _db.Product
                .Select(p => new ProductInfo(p, p.ProductProductPhoto.FirstOrDefault().ProductPhoto.ThumbNailPhoto, lang))
                .Skip(startAt)
                .Take(portion);
        }
        /// <summary>
        /// Returns a product with given ProductId
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <param name="lang">language for product description</param>
        /// <returns></returns>
        public ProductInfo GetById(int id, string lang)
        {
            if (id < 1)
                throw new ArgumentException("Id parameter must be greater than 0");

            return _db.Product
                .Where(p => p.ProductID == id)
                .Select(p => new ProductInfo(p, p.ProductProductPhoto.FirstOrDefault().ProductPhoto.LargePhoto, lang))
                .FirstOrDefault();
        }
        /// <summary>
        /// Returns top amount of most popular bikes buyed by users
        /// </summary>
        /// <param name="amount">Number of bikes to select</param>
        /// <param name="lang">language for product description</param>
        /// <returns></returns>
        public IEnumerable<ProductInfo> GetMostBuyableBikes(int amount, string lang)
        {
            if (amount < 1)
                throw new ArgumentException("Amount parameter must be greater than 0");

            // so useless action to select separate array of Ids, 
            // but SalesOrderDetails table are not refernced to Products table, lol wtf, may be I missed something in DB relations
            var allBikeIds = _db.Product
                .Where(p => p.ProductSubcategory.ProductCategoryID == 1)
                .Select(p => p.ProductID);

            // select separate array of most popular ID's
            var mostPopulardBikeIds = _db.SalesOrderDetail
                .Where(p => allBikeIds.Contains(p.ProductID))
                .GroupBy(p => p.ProductID)
                .OrderByDescending(p => p.Count())
                .Take(amount)
                .Select(p => p.Key);

            return _db.Product
                .Where(p => mostPopulardBikeIds.Contains(p.ProductID))
                .Select(p => new ProductInfo(p, p.ProductProductPhoto.FirstOrDefault().ProductPhoto.ThumbNailPhoto, lang));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="lang">language for product description</param>
        /// <returns></returns>
        public IEnumerable<ProductInfo> Find(string template, string lang)
        {
            if (template == string.Empty || template == null)
                throw new ArgumentException("Template parameter is null or empty");

            return _db.Product
                .Where(p => p.Name.ToLower().Contains(template.ToLower()))
                .Select(p => new ProductInfo(p, p.ProductProductPhoto.FirstOrDefault().ProductPhoto.ThumbNailPhoto, lang));
        }
        /// <summary>
        /// Returns the specified portion of founded info
        /// </summary>
        /// <param name="template"></param>
        /// <param name="portion"></param>
        /// <param name="startAt"></param>
        /// <param name="lang">language for product description</param>
        /// <returns></returns>
        public IEnumerable<ProductInfo> Find(string template, int portion, int startAt, string lang)
        {
            return this.Find(template, lang)
                .Skip(startAt)
                .Take(portion);
        }
        /// <summary>
        /// Returns first founded result
        /// </summary>
        /// <param name="template">search template</param>
        /// <param name="lang">language for product description</param>
        /// <returns></returns>
        public ProductInfo FindFirst(string template, string lang)
        {
            return this.Find(template, lang).FirstOrDefault();
        }
        #endregion

        // IDataService implementation
        #region not-implemented
        // SUMMURY
        // not neccessary to implement this methods, 
        // because of they won't be used in the project


        public bool Remove(int Id)
        {
            //Get()
            throw new NotImplementedException();
        }

        public bool Add(ProductInfo entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(int oldId, ProductInfo entity)
        {
            throw new NotImplementedException();
        }
        #endregion
        
        // IDisposable implementation
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
