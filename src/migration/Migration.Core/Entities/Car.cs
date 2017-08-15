using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Migration.Core.Entities
{
    [Table("migration_car")]
    public class Car
    {

        public Car()
        {
            this.Trips = new Collection<CarTrip>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Model")]
        public Guid CarModelId { get; set; }

        public CarModel Model { get; set; }

        public ICollection<CarTrip> Trips { get; set; }
    }
}
