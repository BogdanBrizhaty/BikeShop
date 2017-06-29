using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class ProductInfo
    {
        public ProductInfo()
        {

        }
        public ProductInfo(Product p, byte[] img, string lang)
        {
            Id = p.ProductID;
            Name = p.Name;
            Description = p.ProductModel.ProductModelProductDescriptionCulture.Where(s => s.CultureID == lang).FirstOrDefault().ProductDescription.Description;
            Price = p.ListPrice;
            Img = img;
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        //[DataMember]
        //public byte[] Thumbnail { get; set; }
        [DataMember]
        public byte[] Img { get; set; }
        //[DataMember]
        //public byte[] FullScale { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
