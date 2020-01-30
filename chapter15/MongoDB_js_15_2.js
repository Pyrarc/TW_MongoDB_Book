//15.2  建立與斷開連接
//---1. 準備工作
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 
//定義資料庫名稱
const dbName = "E-commerce";
 
//用connect方法連接到伺服器
MongoClient.connect(Urls, function(err, client) {
  assert.equal(null, err);
  console.log("Connected successfully to server"); 
  const db = client.db(dbName); 
  client.close();
});
//---2. 設置連接池
//用connect方法連接MongoDB Server
MongoClient.connect(Urls, {
    db:{w:1,native_parser:false},
    server:{
        poolSize:5,
        socketOptions:{connectTimeoutMS:500},
        auto_reconnect:true
    },
    replSet:{}, 
    mongos:{}
    },
    function(err, client) {
          assert.equal(null, err); 
          console.log("Connected successfully to server"); 
          const db = client.db(dbName); 
          client.close();
}
);

