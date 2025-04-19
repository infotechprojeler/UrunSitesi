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
        int Save();
    }
}
