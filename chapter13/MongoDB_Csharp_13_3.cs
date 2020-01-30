13.3.1  新增文檔
//---1. 插入單個文檔
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
    class MongoDB_Csharp_13_3_1_1
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
            // 新建一個文檔物件            
            Document doc = new Document("Name", "Deng")
        	.append("Gender","Male")
        	.append("Tel","18310001000")
        	.append("CustomerSysNo", 11753090)
        	.append("Location", 
                	new Document("City", "Keelung").append("Dist", "Xinyi")
                	);

            // 插入單個文檔
            collection.InsertOne(doc);           
        }
    }
}

//---2. 插入多個文檔
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
    class MongoDB_Csharp_13_3_1_2
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
            // 定義文檔列表
            List<BsonDocument> Docs = new List<BsonDocument>();
            Document doc1 = new Document("Name", "Yun")
        	.append("Gender","Male")
        	.append("Tel","18319991999")
        	.append("CustomerSysNo", 11735490)
        	.append("Location",
                	new Document("City", "Tainan").append("Dist", "Annan")
        	);

            Document doc2 = new Document("Name", "Qiang")
        	.append("Gender","Male")
        	.append("Tel","18318881888")
        	.append("CustomerSysNo", 11704571)
        	.append("Location",
                	new Document("City", " Tainan").append("Dist", "Anping")
        	);

            // 將文檔寫入程式清單中
            Docs.Add(doc1);
            Docs.Add(doc2);
            // 將多個文檔寫入MongoDB
            collection.InsertMany(Docs);
        }
    }
}
   
13.3.2  刪除文檔
//---1. 刪除第1個文檔
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
    class MongoDB_Csharp_13_3_2_1
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
            // 條件是“Tel=18319991999 ”的篩檢程式
            var filter = Builders<BsonDocument>.Filter.Eq("Tel", "18319991999");
            // 刪除單個文檔
            collection.DeleteOne(filter);
        }
    }
}

//---2. 刪除多個文檔
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
    class MongoDB_Csharp_13_3_2_2
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
            // 條件是“Tel=18319991999 ”的篩檢程式
            var filter = Builders<BsonDocument>.Filter.Eq("Tel", "18319991999");
            // 刪除多個文檔
            collection.DeleteMany(filter);
        }
    }
}
   
13.3.3  修改文檔
//---1. 修改第1個文檔
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
    class MongoDB_Csharp_13_3_3_1
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
            var filter = Builders<BsonDocument>.Filter.Eq("CustomerSysNo", 11704571);
            //把“Tel”修改為“18323332333”
            var update = Builders<BsonDocument>.Update.Set("Tel", "18323332333");
            //修改單個文檔
            collection.UpdateOne(filter, update);
        }
    }
}

//---2. 修改多個文檔
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
    class MongoDB_Csharp_13_3_3_2
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
            var filter = Builders<BsonDocument>.Filter.Gt("CustomerSysNo", 11700000);
            var update = Builders<BsonDocument>.Update.Set("Tel", "18323332333");
            //修改多個文檔
            collection.UpdateMany(filter, update);
        }
    }
}

