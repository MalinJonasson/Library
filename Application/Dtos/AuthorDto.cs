namespace Application.Dtos
{
    public class AuthorDto(Guid id, string name)
    {
        public Guid Id { get; set; } = id;
        public string FirstName { get; set; } = name;
    }
}
