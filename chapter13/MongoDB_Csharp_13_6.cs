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
    class MongoDB_Csharp_13_6
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
            //批量操作
            var models = new WriteModel<BsonDocument>[]
            {
                // 插入
                new InsertOneModel<BsonDocument>(new BsonDocument("Name", "Deng")),
                new InsertOneModel<BsonDocument>(new BsonDocument("Name", "Yun")),
                new InsertOneModel<BsonDocument>(new BsonDocument("Name", "Qiang")),
                new InsertOneModel<BsonDocument>(new BsonDocument("Name", "WuGe")),
                // 修改
                new UpdateOneModel<BsonDocument>(
                new BsonDocument("Name", "Deng"),
                new BsonDocument("$set", new BsonDocument("Gender", "Male"))),
                // 刪除
                new DeleteOneModel<BsonDocument>(new BsonDocument("Name", "WuGe")),
                // 替換
                new ReplaceOneModel<BsonDocument>(
                    new BsonDocument("Name", "Qiang"),
                    new BsonDocument("Name", "Qiang").Add("Gender", "Male"))
            };
            collection.BulkWrite(models);
            //不考慮操作順序
            //collection.BulkWrite(models, new BulkWriteOptions { IsOrdered = false });
        }
    }
}

