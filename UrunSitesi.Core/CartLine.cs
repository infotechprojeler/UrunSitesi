using UrunSitesi.Core.Entities;

namespace UrunSitesi.Core
{
    public class CartLine
    {
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
