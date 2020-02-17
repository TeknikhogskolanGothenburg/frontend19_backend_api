using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Repositories
{
    public class ProductsRepository
    {
        private ConfigurationRoot _configuration;
        private IMongoCollection<Product> _collection;

        public ProductsRepository(IConfiguration configuration)
        {
            _configuration = configuration as ConfigurationRoot;
            string collectionName = _configuration["ProductsRepository:ProductsCollectionName"];
            string connectionString = _configuration["Database:ConnectionString"];
            string databaseName = _configuration["Database:Database"];
            MongoClient dbClient = new MongoClient(connectionString);
            var db = dbClient.GetDatabase(databaseName);
            _collection = db.GetCollection<Product>(collectionName);
        }

        internal Product getProductById(string id)
        {
            return _collection.Find<Product>(p => p.Id == id).SingleOrDefault<Product>();
        }

        internal void saveProduct(Product product)
        {
            _collection.InsertOne(product);
        }

        internal void deleteProduct(string id)
        {
            _collection.DeleteOne<Product>(p => p.Id == id);
        }

        internal IEnumerable<Product> getAllProducts()
        {
            return _collection.Find(_ => true).ToEnumerable();
        }

        internal void updateProduct(string id, Product product)
        {
            product.Id = id;
            _collection.ReplaceOne<Product>(p => p.Id == id, product);
        }
    }
}
