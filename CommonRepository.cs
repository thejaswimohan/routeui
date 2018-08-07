namespace SampleAPIProject.Repository
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class CommonRepository<TEntity> : ICommonRepository<TEntity> where TEntity : class
    {
        internal EmployeeEntities context;
        internal DbSet<TEntity> dbSet;

        public CommonRepository(EmployeeEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (entityToDelete == null) return;
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

    }
}