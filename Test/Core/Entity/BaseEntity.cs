using System;

namespace Test.Core.Entity {
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}