using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAL
{
    public class ProductDAL
    {
        OnlineShopDbContext db = null;
        public ProductDAL()
        {
            db = new OnlineShopDbContext();
        }

        public Product GetByProductID(long id)
        {
            return db.Product.Find(id);
        }



        public int Insert(Product entity)
        {
            db.Product.Add(entity);
            db.SaveChanges();
            return 1;
        }

        public int Update(Product entity)
        {
            Product product = GetByProductID(entity.ID);
            product.Name = entity.Name;
            product.MetaTitle = entity.MetaTitle;
            product.Description = entity.Description;
            product.Image = entity.Image;
            product.Price = entity.Price;
            product.PromotionPrice = entity.PromotionPrice;
            product.Quantity = entity.Quantity;
            product.ModifiedBy = entity.ModifiedBy;
            product.ModifiedDate = entity.ModifiedDate;
            product.MetaKeywords = entity.MetaKeywords;
            product.MetaDescriptions = entity.MetaDescriptions;
            db.SaveChanges();
            return 1;
        }

        // Status = 0
        public int Delete(long id)
        {
            Product product = GetByProductID(id);
            product.Status = false;
            db.SaveChanges();
            return 1;
        }

        //Remove from db
        public int Remove(long id)
        {
            Product product = GetByProductID(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return 1;
        }

        public List<Product> GetByTopHot()
        {
            return db.Product.Where(x => x.TopHot >= DateTime.Now).OrderByDescending(x => x.TopHot).ToList();
        }

        public List<Product> GetByNewProduct()
        {
            return db.Product.Where(x => x.CreatedDate != null).OrderBy(x => x.CreatedDate).Take(10).ToList();
        }

        public List<Product> GetByCategory(long id)
        {
            return db.Product.Where(x => x.CategoryID == id).ToList();
        }

        public List<Product> GetBySelectedProduct(long id)
        {
            Product product = GetByProductID(id);
            return db.Product.Where(x => x.MetaKeywords.Contains(product.MetaKeywords)).ToList();
        }

        public List<Product> GetAllProduct()
        {
            return db.Product.ToList();
        }

        public List<Product> GetByPromoteProduct()
        {
            return db.Product.Where(x => x.PromotionPrice > 0).Take(10).ToList();
        }

        public decimal? GetByPruchasePrice(long id, int quanity)
        {
            Product product = GetByProductID(id);
            return (product.PromotionPrice == null && quanity > 0) ? product.Price*quanity : (product.Price - product.PromotionPrice)*quanity;
        }

        public List<Product> GetByHighPromoteProduct()
        {
            return db.Product.Where(x=>x.PromotionPrice > 0).OrderByDescending(x => x.PromotionPrice).ToList();
        }

        public List<Product> GetByBestSeller()
        {
            return db.Product.Where(x => x.ViewCout > 0).OrderByDescending(x => x.ViewCout).Take(10).ToList();
        }

    }
}
