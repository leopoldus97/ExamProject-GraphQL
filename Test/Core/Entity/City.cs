using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Core.Entity {
    [Table("city", Schema = "business")]
    public partial class City : BaseEntity {
        public string Name { get; set; }
        public int? Population { get; set; }
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }
    }
}