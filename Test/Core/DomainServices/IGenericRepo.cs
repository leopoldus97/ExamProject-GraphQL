using System.Collections.Generic;
using Test.Core.Entity;

namespace Test.Core.DomainServices {
    public interface IGenericRepo<T> where T : BaseEntity {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Insert(T entity);
        T Update(T entity);
        void Delete(int id);
    }
}