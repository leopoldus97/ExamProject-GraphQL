using System;

namespace Test.Core.Entity {
    public interface IEntity {
        int Id { get; set; }
        DateTime? CreationDate { get; set; }
    }
}