﻿using MDLibrary.Application.Interfaces;
using MDLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MDLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MDLibrary.Infrastructure.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext context;
        //public List<Author> Authors = new List<Author>();

        public AuthorService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddAuthor(Author author)
        {
            context.Add(author);
            context.SaveChanges();
        }

        public IList<Author> GetAllAuthors()
        {

            return context.Author.ToList();
            //return Authors;
        }

        public Author GetAuthorById(int id)
        {
            return context.Author.Find(id);
        }

        public void RemoveAuthor(int id)
        {
            var author = context.Author.Find(id);
            context.Author.Remove(author);
            context.SaveChanges();
        }

        public void EditAuthor(Author author)
        {
            context.Author.Update(author);
            context.SaveChanges();
        }

        public bool CanRemoveAuthor(int id)
        {
            var author = context.Author.Include(a => a.WrittenBooks)
                .FirstOrDefault(a => a.ID == id);

            return author.WrittenBooks.Count < 1;
            //return context.Author.Contains()
        }
    }
}
