using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB
{
    class Program
    {
        class Coll1
        {
            public ObjectId id { get; set; }
            public string Name { get; set; }
            public string Group { get; set; }
        }

        static void Main(string[] args)
        {
            string connectionString = "mongodb://localhost";

            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("Test");
            Console.ReadLine();
        }

        void Collection1Get(IMongoDatabase database)
        {
            IMongoCollection<BsonDocument> col = database.GetCollection<BsonDocument>("users");
        }

        private static async Task GetCollectionsNames(MongoClient client)
        {
            using (var cursor = await client.ListDatabasesAsync())
            {
                var dbs = await cursor.ToListAsync();
                foreach (var db in dbs)
                {
                    Console.WriteLine("В базе данных {0} имеются следующие коллекции:", db["name"]);

                    IMongoDatabase database = client.GetDatabase(db["name"].ToString());

                    using (var collCursor = await database.ListCollectionsAsync())
                    {
                        var colls = await collCursor.ToListAsync();
                        foreach (var col in colls)
                        {
                            Console.WriteLine(col["name"]);
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
