using MDLibrary.Application.Interfaces;
using MDLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MDLibrary.Infrastructure.Service
{
    public class AuthorService : IAuthorService
    {
        public List<Author> Authors = new List<Author>();

        public void AddAuthor(Author author)
        {
            Authors.Add(author);
        }

        public IList<Author> GetAllAuthors()
        {
            return Authors;
        }

        public void RemoveAuthor(int id)
        {
            Authors.RemoveAll(a => a.ID == id);
        }
    }
}
