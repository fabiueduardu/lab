using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Migration.Core.Entities
{
    [Table("migration_cartrip")]
    public class CarTrip
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string From { get; set; }

        [Required]
        [StringLength(100)]
        public string To { get; set; }

        [Required]
        [ForeignKey("Car")]
        public Guid CarId { get; set; }

        public Car Car { get; set; }
    }
}
