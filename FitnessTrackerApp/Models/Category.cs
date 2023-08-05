using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FitnessTrackerApp.Models
{
    public class Category
    {
        // [Key] //because Id can be primary key
        public int Id { get; set; } = 1;
        //[Required] //because name cannot be null
        public string Name { get; set; } = "Samadhi";

        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage ="Display Order must be between 1 and 100 only!!")]
        public int DisplayOrder { get; set; } = 2;

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
