using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;

namespace UrunSitesi.Service
{
    public class Service<T> : IService<T> where T : class, IEntity, new() // Service<T> sınıfı generic çalışacak(tüm class lar gelebilir), IService<T> interface indeki tüm metotları kullanacak, where T : (buraya parametre olarak gönderilecek olan T şu özelliklere sahip olmalı:) class (T bir class olmalı (int, string vb olamaz!)), T IEntity interfaceinden implement edilmiş olmalı, new() : T new lenebilir bir nesne olmalı(class gibi)
    {
        private readonly DatabaseContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Service(DatabaseContext dbContext)
        {
            _dbContext = dbContext; // yukarıdaki boş _dbContext i doldur
            _dbSet = _dbContext.Set<T>(); // yukarıdaki _dbContext i gönderilen T parametresindeki class için ayarla.
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Find(int id)
        {
           return _dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefaultAsync(expression);
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
