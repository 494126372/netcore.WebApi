using Microsoft.Extensions.Configuration;
using MongoDB.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.API.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;
        public BookService(IBookstoreDatabaseSettings settings, IConfiguration config)
        {
            //var client = new MongoClient(settings.ConnectionString);
            //var database = client.GetDatabase(settings.DatabaseName);
            //_books = database.GetCollection<Book>(settings.BooksCollectionName);
            string str = config.GetConnectionString("MongoDBConnString");
            var client = new MongoClient(str);
            var database = client.GetDatabase("BookstoreDb");
            _books = database.GetCollection<Book>("Books");

        }
        public List<Book> Get() => _books.Find(e=>true).ToList();
        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }
        public void Remove(Book book)
        {
            _books.DeleteOne(e => e.Id == book.Id);
        }

        public void Update(string id, Book book) {

            _books.ReplaceOne(e => e.Id == id, book);
        }
        public void Remove(string id)
        {
            _books.DeleteOne(e => e.Id == id);
        }
       
    }
}
