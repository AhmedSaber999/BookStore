using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class AuthoRepository : IBookRepository<Auther>
    {
        List<Auther> authors;
        public AuthoRepository()
        {
            authors = new List<Auther>()
            {
                new Auther { id = 1, name = "Ahmed Saber"},
                new Auther { id = 2, name = "Muhammed Saber"},
                new Auther { id = 3, name = "Tokaa Saber"},
            };
        }
        public void Add(Auther entity)
        {
            entity.id = authors.Max(a => a.id) + 1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            authors.Remove(Find(id));
        }

        public Auther Find(int id)
        {
            Auther author = authors.SingleOrDefault(a => a.id == id) ;
            return author;
        }

        public IList<Auther> List()
        {
            return authors;
        }

        public void Update(int id, Auther entity)
        {
            Auther author = Find(id);
            author.name = entity.name;
        }
    }
}
