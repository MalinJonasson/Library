using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Author
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name can be maximum 50 characters")]
        public string Name { get; set; }

        public Author(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Author(){}

    }
}
