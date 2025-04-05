using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrunSitesi.Data;

namespace UrunSitesi.MVCWebUI.ViewComponents
{
    public class Categories : ViewComponent
    {
        // View Components kullanmak için ana dizindeki View > Shared klasörü içine Components isminde bir klasör eklemliyiz. Sonrasında bu klasöre de Categories isminde bir klasör ekliyoruz. Farklı komponentler kullanacaksak onlar için de bu şekilde yapmalıyız. Son olarak Categories isimli klasörün içine Default.cshtml isminde boş bir view oluşturuyoruz. Buradan veritabanından çekeceğimiz kategori listesini bu view da listeleteceğiz.
        private readonly DatabaseContext _dbContext;

        public Categories(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync() // InvokeAsync metodu asenkron bir şekilde verileri shared > components > categories altındaki default view ına gönderecek
        {
            return View(await _dbContext.Categories.Where(c => c.IsTopMenu && c.IsActive).ToListAsync()); // view çalıştırılırken içinde kategori listeleyeceğimiz için modele gerekli veriyi burada gönderiyoruz.
        }
    }
}
