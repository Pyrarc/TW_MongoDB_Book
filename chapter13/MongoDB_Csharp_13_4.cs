13.4.1  限制查詢結果集大小
//---1. 查詢所有文檔的兩種方法
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
    class MongoDB_Csharp_13_4_1_1
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
            //---ToList()方法---
            //返回所有文檔
            var documents = collection.Find(new BsonDocument()).ToList();
            Console.WriteLine(documents.ToJson());
            //---ToEnumerable()方法---
            //取得查詢文檔的游標
            var cursor = collection.Find(new BsonDocument()).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                Console.WriteLine(document);
            }
            Console.Read();
        }
    }
}
//---2. 限制查詢結果集的大小
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
    class MongoDB_Csharp_13_4_1_2
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
            //取得兩個文檔
            var cursor = collection.Find(new BsonDocument()).Limit(2).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                Console.WriteLine(document);
            }
            Console.Read();
        }
    }
}
   
13.4.2  限制查詢返回的欄位
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
    class MongoDB_Csharp_13_4_2
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
            //指定只顯示“Name”和“Tel”欄位，排除“_id”欄位
            var projection = Builders<BsonDocument>.Projection
                     .Include("Name")
                     .Include("Tel")
                     .Exclude("_id");
            var cursor = collection.Find(new BsonDocument()).Project(projection).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                Console.WriteLine(document);
            }
            Console.Read();
        }
    }
}
  
13.4.3  按條件進行查詢
//---1. 單個條件查詢
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
    class MongoDB_Csharp_13_4_3_1
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
            var filter = Builders<BsonDocument>.Filter.Eq("CustomerSysNo", 11735490);
            var cursor = collection.Find(filter).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                Console.WriteLine(document);
            }
            Console.Read();
        }
    }
}
//---2. 多個條件查詢
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
    class MongoDB_Csharp_13_4_3_2
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
            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder
                            //大於11700000
                            .Gt("CustomerSysNo", 11700000)
                            //小於等於11760000
                            & filterBuilder.Lte("CustomerSysNo", 11760000);
            var cursor = collection.Find(filter).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                Console.WriteLine(document);
            }
            Console.Read();
        }
    }
}
   
13.4.4  將查詢結果分頁顯示
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
    class MongoDB_Csharp_13_4_4
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
            //---2. 測試分頁函數
            var filterBuilder = Builders<BsonDocument>.Filter;
            // CustomerSysNo大於1170123且小於等於11760000
            var filter = filterBuilder.Gt("CustomerSysNo", 1170123)
                            & filterBuilder.Lte("CustomerSysNo", 11760000);
            //按照CustomerSysNo 降冪排列
            var sort = Builders<BsonDocument>.Sort.Descending("CustomerSysNo");
            var cursor = findByPage(collection, filter, sort, 1, 2);
            
            foreach (var document in cursor.ToEnumerable())
            {
                Console.WriteLine(document);
            }
            
            Console.Read();
        }
        //---1. 編寫分頁函數
        public static IAsyncCursor<BsonDocument> findByPage(
            IMongoCollection<BsonDocument> mongoCollection, FilterDefinition<BsonDocument> filter, SortDefinition<BsonDocument> orderBy, int pageNo, int pageSize)
        {
            return mongoCollection
                    //查找
                    .Find(filter)
                    //排序
                    .Sort(orderBy)
                    //取得第pageNo頁的資料
                    .Skip((pageNo - 1) * pageSize).Limit(pageSize).ToCursor();
        }
    }
  
}
   
13.4.5  使用聚合方法查詢文檔
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
    class MongoDB_Csharp_13_4_5
    {
        static void Main(string[] args)
        {
            //獲取連接字串
            string connStr = ConfigurationManager.ConnectionStrings["strCon"].ToString();
            var mongourl = new MongoUrl(connStr);
            MongoClient client = new MongoClient(mongourl);
            // 獲取資料庫名
            var mongoDatabase = client.GetDatabase(mongourl.DatabaseName);
            // 獲取集合Carts
            var collection = mongoDatabase.GetCollection<BsonDocument>("Carts");
            // 定義Group的字串格式
            string strGroup = "{ '_id':'total',  'total':{ $sum:'$Quantity'}, 'avgQuantity': { $avg: '$Quantity' },'maxQuantity':{ $max:'$Quantity' } ,'minQuantity':{ $min:'$Quantity'}}";
            // 聚合
            var aggregate = collection.Aggregate()
           .Group(BsonDocument.Parse(strGroup))
           .ToCursor();
            foreach (var document in aggregate.ToEnumerable())
            {
                Console.WriteLine(document); 
            }
            Console.Read();
        }
    }
}
  
13.4.6  應用索引查詢
//---1. 一般索引
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
    class MongoDB_Csharp_13_4_6_1
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
                      
            //“Name”欄位設置昇冪的索引
            collection.Indexes.CreateOne(new BsonDocument("Name", 1));
            
            //“Name”欄位設置降冪的索引
            collection.Indexes.CreateOne(new BsonDocument("Name", -1));
            
            //“Name”欄位設置昇冪索引，對“CustomerSysNo”欄位設置降冪索引
            var keys = Builders<BsonDocument>.IndexKeys
                       .Ascending("Name").Descending("CustomerSysNo");
            collection.Indexes.CreateOne(keys);
        }
    }
}
//---2. 文本索引
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
    class MongoDB_Csharp_13_4_6_2
    {
        static void Main(string[] args)
        {
            //獲取連接字串
            string connStr = ConfigurationManager.ConnectionStrings["strCon"].ToString();
            var mongourl = new MongoUrl(connStr);
            MongoClient client = new MongoClient(mongourl);
            // 獲取資料庫名
            var mongoDatabase = client.GetDatabase(mongourl.DatabaseName);
            // 獲取集合Product
            var collection = mongoDatabase.GetCollection<BsonDocument>("Product");
            //創建文本索引
            collection.Indexes.CreateOne(BsonDocument.Parse("{'ProductName':'text'}"));
            //查詢“ProductName”欄位包含關鍵字“Gold”的所有文檔
            var filter = Builders<BsonDocument>.Filter.Text("Gold");
            var cursor = collection.Find(filter).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                Console.WriteLine(document);
            }
            Console.Read();
        }
    }
}

