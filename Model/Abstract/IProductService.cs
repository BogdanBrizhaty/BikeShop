using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IProductService : IDataService<ProductInfo>
    {
        IEnumerable<ProductInfo> GetMostBuyableBikes(int amount, string lang);
        IEnumerable<ProductInfo> Find(string template, string lang);
        ProductInfo FindFirst(string template, string lang);
        ProductInfo GetById(int id, string lang);
        IEnumerable<ProductInfo> GetAll(string lang);
        IEnumerable<ProductInfo> Get(int portion, int startAt, string lang);
        //
    }
}
