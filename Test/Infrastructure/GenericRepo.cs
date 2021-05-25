using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Test.Core.DomainServices;
using Test.Core.Entity;

namespace Test.Infrastructure {
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        protected readonly DatabaseContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public GenericRepo(DatabaseContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public void Delete(int id)
        {
            T entity = entities.FirstOrDefault(s => s.Id == id);
            entities.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T GetById(int id)
        {
            return entities.FirstOrDefault(s => s.Id == id);
        }

        public T Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            entities.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            entities.Update(entity);
            context.SaveChanges();
            return entity;
        }
    }
}