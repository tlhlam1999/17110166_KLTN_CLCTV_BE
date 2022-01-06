﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Shop.repositories.RepositoryImpl
{
    public abstract class GeneralRepositoryImpl<TEntity, TContext>
            where TEntity : class
            where TContext : DbContext
    {
        private readonly TContext context;

        public GeneralRepositoryImpl(TContext context)
        {
            this.context = context;
        }

        public TEntity Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
            return entity;
        }

        public List<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public TEntity Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public TEntity Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public TEntity Delete(int id)
        {
            var entity = context.Set<TEntity>().Find(id);
            if (entity == null)
            {
                return entity;
            }
            context.Set<TEntity>().Remove(entity);
            context.SaveChanges();
            return entity;
        }
    }
}
