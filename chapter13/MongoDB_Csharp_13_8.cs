using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.GridFS;
namespace MongoDBTest
{
    class MongoDB_Csharp_13_8
    {
        static void Main(string[] args)
        {
            //獲取連接字串
            string connStr = ConfigurationManager.ConnectionStrings["strCon"].ToString();
            var mongourl = new MongoUrl(connStr);
            MongoClient client = new MongoClient(mongourl);
            // 獲取資料庫名
            var mongoDatabase = client.GetDatabase(mongourl.DatabaseName);           
            
            //檔路徑
            string filePath = "D:\\MongoDB.pdf";
            //讀取文件流
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            var bucket = new GridFSBucket(mongoDatabase, new GridFSBucketOptions
            {
                //設置Bucket 名稱，此名稱將作為集合的首碼名
                BucketName = "fspdf",
                //設置ChunkSize
                ChunkSizeBytes = 358400,
                //設置寫安全等級，需要確認資料成功寫入到大多數節點才算成功
                WriteConcern = WriteConcern.WMajority,
                //只從 secondary 節點讀數據
                ReadPreference = ReadPreference.Secondary
            });
            var options = new GridFSUploadOptions
            {
                ChunkSizeBytes = 358400,
                Metadata = new BsonDocument
                {
                    { "type", "pdf" },
                    { "copyrighted", true }
                }
            };
            //上傳
            ObjectId fileid = bucket.UploadFromStream("filename", fileStream, options);
            fileStream.Close();
            Console.WriteLine("The fileId of the uploaded file is: " + fileid.ToString());
            Console.Read();                
            
        }
    }
}
//---1. 用ObjecId為條件來下載
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.GridFS;
namespace MongoDBTest
{
    class MongoDB_Csharp_13_8_1
    {
        static void Main(string[] args)
        {
            //獲取連接字串
            string connStr = ConfigurationManager.ConnectionStrings["strCon"].ToString();
            var mongourl = new MongoUrl(connStr);
            MongoClient client = new MongoClient(mongourl);
            // 獲取資料庫名
            var mongoDatabase = client.GetDatabase(mongourl.DatabaseName);
            string filePath = @"D:\\download\MongoDB.pdf";
            FileStream fileStream = new FileStream(filePath, FileMode.Append);
            var bucket = new GridFSBucket(mongoDatabase, new GridFSBucketOptions
            {
                BucketName = "fspdf",
                ChunkSizeBytes = 358400,
                WriteConcern = WriteConcern.WMajority,
                ReadPreference = ReadPreference.Secondary
            });
            ObjectId fileId = new ObjectId("5ae2845f19a65c1258f280b9");
            //下載
            bucket.DownloadToStream(fileId, fileStream);
            fileStream.Close();
        }
    }
}
//---2. 用檔案名為條件來下載
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.GridFS;
namespace MongoDBTest
{
    class MongoDB_Csharp_13_8_2
    {
        static void Main(string[] args)
        {
            //獲取連接字串
            string connStr = ConfigurationManager.ConnectionStrings["strCon"].ToString();
            var mongourl = new MongoUrl(connStr);
            MongoClient client = new MongoClient(mongourl);
            // 獲取資料庫名
            var mongoDatabase = client.GetDatabase(mongourl.DatabaseName);
            string filePath = @"D:\\download\MongoDB_pdf.pdf";
            
            FileStream fileStream = new FileStream(filePath, FileMode.Append);
            var bucket = new GridFSBucket(mongoDatabase, new GridFSBucketOptions
            {
                BucketName = "fspdf",
                ChunkSizeBytes = 358400,
                WriteConcern = WriteConcern.WMajority,
                ReadPreference = ReadPreference.Secondary
            });
            var options = new GridFSDownloadByNameOptions
            {
                //設置下載版本
                Revision = 0
            };
            //下載
            bucket.DownloadToStreamByName("filename", fileStream, options);
            Console.WriteLine("Download success ");
            fileStream.Close();
            Console.Read();
        }
    }
}
//---3. 查找GridFS中的文檔
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.GridFS;
namespace MongoDBTest
{
    class MongoDB_Csharp_13_8_3
    {
        static void Main(string[] args)
        {
            //獲取連接字串
            string connStr = ConfigurationManager.ConnectionStrings["strCon"].ToString();
            var mongourl = new MongoUrl(connStr);
            MongoClient client = new MongoClient(mongourl);
            // 獲取資料庫名
            var mongoDatabase = client.GetDatabase(mongourl.DatabaseName);
            var bucket = new GridFSBucket(mongoDatabase, new GridFSBucketOptions
            {
                BucketName = "fspdf",
                ChunkSizeBytes = 358400,
                WriteConcern = WriteConcern.WMajority,
                ReadPreference = ReadPreference.Secondary
            });
            var filter = Builders<GridFSFileInfo>.Filter.Eq("metadata.type", "pdf");
            var gridfsList = bucket.Find(filter).ToList();
            Console.WriteLine(gridfsList.ToJson());
            
            Console.Read();
            //通過GridFS刪除文檔
            ObjectId fileId = new ObjectId("5ae2845f19a65c1258f280b9");
            bucket.Delete(fileId);
        }
    }
}

