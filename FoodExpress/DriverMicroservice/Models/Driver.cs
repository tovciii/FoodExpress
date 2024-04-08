using System.ComponentModel.DataAnnotations.Schema;

namespace FoodExpress.DriverMicroservice.Models
{
    [Table("drivers")]
    public class Driver
    {
        [Column("driver_id")]
        public int DriverId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("available")]
        public string Available { get; set; }
    }
}
