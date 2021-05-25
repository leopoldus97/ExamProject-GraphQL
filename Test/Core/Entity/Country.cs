using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Core.Entity {
    [Table("country", Schema = "business")]
    public partial class Country : BaseEntity {
        public string Name { get; set; }
        public string Continent { get; set; }
    }
}