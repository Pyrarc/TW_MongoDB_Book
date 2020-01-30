APP.CONFIG
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5.2" />
    </startup>
    <connectionStrings>
         <add name="strCon" connectionString="mongodb://<mongodb_user>:<mongodb_pwd>@<host1>:<port>,<host2>:<port>,<host3>:<port>/?replicaSet=myRepSet"/>
    </connectionStrings>
</configuration>
   
遍歷列印資料庫的程式
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MongoDBTest
{
    class MongoDB_Csharp_13_2
    {
        static void Main(string[] args)
        {
            //獲取連接字串
            string connStr = ConfigurationManager.ConnectionStrings["strCon"].ToString();
            //獲取MongoUrl對象
            var mongourl = new MongoUrl(connStr);
            //獲取MongoClient對象
            MongoClient client = new MongoClient(mongourl);
            using (IAsyncCursor<BsonDocument> cursor = client.ListDatabases())
            {
                while (cursor.MoveNext())
                {
                    foreach (var doc in cursor.Current)
                    {
                        // 列印資料庫名
                        Console.WriteLine(doc["name"]);
                    }
                }
            }
            Console.Read();
        }
    }
}

