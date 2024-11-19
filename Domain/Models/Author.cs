namespace Domain.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Author(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Author(){}

    }
}
