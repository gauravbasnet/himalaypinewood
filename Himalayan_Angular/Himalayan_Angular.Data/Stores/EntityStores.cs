using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himalayan_Angular.Data.Stores
{
    internal class EntityStore<TEntity> where TEntity : class
    {
        /// <summary>
        /// Context for the store
        /// 
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public DbContext Context { get; private set; }

        /// <summary>
        /// Used to query the entities
        /// 
        /// </summary>
        public IQueryable<TEntity> EntitySet => DbEntitySet;

        /// <summary>
        /// EntitySet for this store
        /// 
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public DbSet<TEntity> DbEntitySet { get; private set; }

        /// <summary>
        /// Constructor that takes a Context
        /// 
        /// </summary>
        /// <param name="context"/>
        public EntityStore(DbContext context)
        {
            Context = context;
            DbEntitySet = context.Set<TEntity>();
        }

        /// <summary>
        /// FindAsync an entity by ID
        /// 
        /// </summary>
        /// <param name="id"/>
        /// <returns/>
        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            return DbEntitySet.FindAsync(id);
        }

        /// <summary>
        /// Insert an entity
        /// 
        /// </summary>
        /// <param name="entity"/>
        public void Create(TEntity entity)
        {
            DbEntitySet.Add(entity);
        }

        /// <summary>
        /// Mark an entity for deletion
        /// 
        /// </summary>
        /// <param name="entity"/>
        public void Delete(TEntity entity)
        {
            DbEntitySet.Remove(entity);
        }

        /// <summary>
        /// Update an entity
        /// 
        /// </summary>
        /// <param name="entity"/>
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                return;
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
