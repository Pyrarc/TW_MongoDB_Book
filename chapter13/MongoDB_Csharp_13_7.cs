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
    class MongoDB_Csharp_13_7
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
            // 顯示欄位：Name、Tel、CustomerSysNo、Quantity、product
            var projection = Builders<BsonDocument>.Projection
                 .Include("Name")
                 .Include("Tel")
                 .Include("CustomerSysNo")
                 .Include("Quantity")
                 .Include("product");
            //Name為Yun Deng
            var filter = Builders<BsonDocument>.Filter.Eq("Name", "Yun Deng");
            var aggregate = collection.Aggregate()
                 .Project(projection)
                 .Match(filter)
                 /*
                   連接購物車(Carts)，連接欄位為CustomerSysNo
                   lookup屬性值格式如：lookup (from,LocalField,foreignField,as)
                     */
                 .Lookup("Carts", "CustomerSysNo", "CustomerSysNo", "MyCarts").ToCursor();
            foreach (var document in aggregate.ToEnumerable())
            {
                Console.WriteLine(document);
            }
            Console.Read();
        }
    }
}

