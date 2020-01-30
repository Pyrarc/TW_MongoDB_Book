using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MongoDBTest
{
    class MongoDB_Csharp_13_5
    {
        static void Main(string[] args)
        {
            //獲取連接字串
            string connStr = ConfigurationManager.ConnectionStrings["strCon"].ToString();
            var mongourl = new MongoUrl(connStr);
            MongoClient client = new MongoClient(mongourl);
            // 獲取資料庫名
            var mongoDatabase = client.GetDatabase(mongourl.DatabaseName);           
            // 獲取集合Members
            var collection = mongoDatabase.GetCollection<BsonDocument>("Members");
            var filter = Builders<BsonDocument>
                .Filter.Regex("Name", new BsonRegularExpression("Yun", "i"));
            var cursor = collection.Find(filter).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                Console.WriteLine(document);
            }
            Console.Read();
        }
    }
}

