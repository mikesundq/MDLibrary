using MDLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDLibrary.Application.Interfaces
{
    public interface IAuthorService
    {
        public void AddAuthor(Author author);
        public void RemoveAuthor(int id);
        public IList<Author> GetAllAuthors();

        public Author GetAuthorById(int id);
    }
}
