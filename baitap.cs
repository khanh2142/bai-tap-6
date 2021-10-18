using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAL
{
    class ProductDAL
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

        public List<Product> GetByName(string name)
        {
            return db.Product.Where(x => x.Name == name).ToList();
        }

        public List<Product> GetByCode(string code)
        {
            return db.Product.Where(x => x.Code == code).ToList();
        }

        public List<Product> GetByDate(DateTime date)
        {
            return db.Product.Where(x => x.CreatedDate == date).ToList();
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
            return db.Product.OrderByDescending(x => x.TopHot).Take(10).ToList();
        }

        public List<Product> GetByNewProduct()
        {
            return db.Product.OrderBy(x => x.CreatedDate).Take(10).ToList();
        }

        public List<Product> GetByCategory(long id)
        {
            return db.Product.Where(x => x.CategoryID == id).ToList();
        }

        public List<Product> GetBySelectedProduct(long id)
        {
            Product product = GetByProductID(id);
            return db.Product.Where(x => x.MetaKeywords == product.MetaKeywords).ToList();
        }

        public List<Product> GetAllProduct()
        {
            return db.Product.ToList();
        }

        public List<Product> GetByPromoteProduct()
        {
            return db.Product.Where(x => x.PromotionPrice > 0).Take(10).ToList();
        }

        public decimal? GetByPruchasePrice(long id)
        {
            Product product = GetByProductID(id);
            return product.PromotionPrice == null ? product.Price : product.Price - product.PromotionPrice;
        }

        public List<Product> GetByHighPromoteProduct()
        {
            return db.Product.OrderByDescending(x => x.PromotionPrice).ToList();
        }

        public List<Product> GetByBestSeller()
        {
            return db.Product.OrderByDescending(x => x.ViewCout).Take(10).ToList();
        }

    }
}
