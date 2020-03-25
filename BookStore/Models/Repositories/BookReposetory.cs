using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class BookReposetory : IBookRepository<Book>
    {
        List<Book> books;
        public BookReposetory()
        {
            books = new List<Book>()
            {
                new Book
                {
                    id = 1, title = "math", description = "not yet"
                },
                new Book
                {
                    id = 2, title = "programming", description = "not yet"
                },
                new Book
                {
                    id = 3, title = "design", description = "not yet"
                }
            };
        }
        public void Add(Book entity)
        {
            entity.id = books.Max(b => b.id) + 1;
            books.Add(entity) ;
        }

        public void Delete(int id)
        {
            books.Remove(Find(id));
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public void Update(int id, Book entity)
        {
            var book = Find(id);
            book.title = entity.title;
            book.description = entity.description;
            book.auther = entity.auther;
        }
    }
}
