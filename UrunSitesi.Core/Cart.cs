using UrunSitesi.Core.Entities;

namespace UrunSitesi.Core
{
    public class Cart
    {
        private List<CartLine> products = new();
        public List<CartLine> Products => products;
        public void AddProduct(Product product, int quantity)
        {
            var prd = products.Where(i => i.Product.Id == product.Id).FirstOrDefault();
            if (prd == null)
            {
                products.Add(new CartLine { 
                    Product = product, 
                    Quantity = quantity
                });
            }
            else
                prd.Quantity += quantity;
        }
    }
}
