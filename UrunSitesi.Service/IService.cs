using System.Linq.Expressions;

namespace UrunSitesi.Service
{
    // Repository Pattern : Kendi crud metotlarımızı yazmamızı sağlar
    public interface IService<T> // buradaki T type tan geliyor ve bir tip gelmesini bekliyor
    {
        List<T> GetAll(); // metot imzası.
        List<T> GetAll(Expression<Func<T, bool>> expression); // x=>x.name == "elektronik". Geriye sorgu sonucunda gönderilen class ın db deki listesini döner
        void Add(T entity);
        T Find(int id);
        T Get(Expression<Func<T, bool>> expression); // geriye sorgu sunucunda 1 kayıt döner
        void Update(T entity);
        void Delete(T entity);
        int Save(); // save metodu ef deki savechanges in yaptığı işi yapıp geriye etkilenen kayıt sayısını dönmesi içün.

        // Asenkron işlemler
        Task AddAsync(T entity); //Asenkron ekleme, Task AddAsync de geriye bir şey dönmeyeceğimiz için normal void metoda karşılık gelen asenkron yapıyı kullandık
        Task<int> SaveAsync(); // Asenkron kaydetme, Task<int> bu metodun geriye int tipinde veri dönmesini sağlar
        Task<List<T>> GetAllAsync(); // Asenkron listeleme
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
    }
}
