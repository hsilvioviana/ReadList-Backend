using Microsoft.EntityFrameworkCore;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;

namespace ReadList.Infraestructure.Repositories
{
    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel: BaseModel, new()
    {
        protected readonly DbContext Db;
        protected readonly DbSet<TModel> DbSet;

        protected BaseRepository(DbContext context)
        {
            Db = context;
            DbSet = Db.Set<TModel>();
        }

        public virtual async Task<List<TModel>> Search() =>
            await DbSet.AsNoTracking().ToListAsync();

        public virtual async Task<TModel> Find(Guid id) =>
            await DbSet.FindAsync(id);        

        public virtual async Task Create(TModel model)
        {
            DbSet.Add(model);
            await SaveChanges();
        }

        public virtual async Task CreateMany(List<TModel> models)
        {
            DbSet.AddRange(models);
            await SaveChanges();
        }

        public virtual async Task Update(TModel model)
        {
            DbSet.Update(model);
            await SaveChanges();
        }

        public virtual async Task Delete(Guid id)
        {
            Db.ChangeTracker.Clear();
            DbSet.Remove(new TModel { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Db?.Dispose();
        }
    }
}
