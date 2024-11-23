﻿namespace Domain.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public Guid AuthorId { get; set; } 
        public Author Author { get; set; }

        public Book() { }
        public Book(Guid id, string title, string description, Guid authorId)
        {
            Id = id;
            Title = title;
            Description = description;
            AuthorId = authorId;
        }
    }
}
