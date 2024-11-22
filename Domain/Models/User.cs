namespace Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

       // public User(Guid id, string name, string password)
       // {
       //     Id = id;
       //     Name = name;
       //     Password = password;
       // }
    }
}
