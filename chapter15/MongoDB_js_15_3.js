//15.3.1 新增文檔
//1. 新增文檔
//15.3  應用與操作
//15.3.1  新增文檔
//---1. 新增文檔
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//第1步，創建一個新增文檔的方法
//定義函數運算式，用於操作資料庫並返回結果
//參數：資料庫名、回呼函數
var insertData = function(db, callback) {  
//獲得指定的集合 
    var collection = db.collection("Members");
    //插入資料
    var data = {"Name":'Duoduo Ling',
                  "Gender" : "Female",
                  "Tel" : "13800138001",	
                  "CustomerSysNo" : 11762066,
 			  "Location" : {
                       "City" : "Taipei",
                	    "Dist" : "Songshan"
                        }                
    };
    collection.insert(data, function(err, result) { 
        //如果存在錯誤
        if(err)
        {
            console.log('Error:'+ err);
        } 
        //調用傳入的回檔方法，將操作結果返回
        callback(err,result);
    });
}
//第2步，連接MongoDB資料庫並調用新增文檔方法
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    console.log("連接成功！");
    const db = client.db(dbName);
    //執行插入資料操作，調用自訂方法
    insertData(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});

//------------------------------------------------------------------------------------------

//2. 新增文檔陣列
//15.3  應用與操作
//15.3.1  新增文檔
//---2. 新增文檔陣列
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//第1步，創建一個新增文檔的方法
//定義函數運算式，用於操作資料庫並返回結果
//參數：資料庫名、回呼函數
var insertData = function(db, callback) {  
//獲得指定的集合 
    var collection = db.collection("Members");
    //插入資料
    //定義文檔陣列
    var datas = [{"Name":'Sam Liu,     
            "Gender" : "Male",
            "Tel" : "13800138003",
            "CustomerSysNo" : 5109983,
            "Location" : { "City" : "Taipei", "Dist" : "Datong" }
            },{"Name":'Jerry Deng',
            "Gender" : "Male",
            "Tel" : "13800138004",
            "CustomerSysNo" : 10676104,
            "Location" : { "City" : "Taipei", "Dist" : "Wanhua" }
            }
    ];
    collection.insert(datas, function(err, result) { 
        //如果存在錯誤
        if(err)
        {
            console.log('Error:'+ err);
        } 
        //調用傳入的回檔方法，將操作結果返回
        callback(err,result);
    });
}
//第2步，連接MongoDB資料庫並調用新增文檔方法
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    console.log("連接成功！");
    const db = client.db(dbName);
    //執行插入資料操作，調用自訂方法
    insertData(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});
    
//------------------------------------------------------------------------------------------
    
//15.3.2 刪除文檔
//1. collection. deleteOne()方法
//15.3  應用與操作
//15.3.2  刪除文檔
//---1. collection. deleteOne()方法
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//定義函數運算式，用於操作資料庫並返回結果
//定義刪除資料的方法
var deleteData = function(db, callback) {  
    //獲得指定的集合 
    var collection = db.collection("Members");
    //定義要刪除資料的條件，"Name"為"Sam Liu"的用戶將被刪除
    var  where={"Name":"Sam Liu"};
    collection.deleteOne(where,function(err, result) { 
        //如果存在錯誤
        if(err)
        {
            console.log('Error:'+ err);
        };
        //調用傳入的回檔方法，將操作結果返回
        callback(err,result);
    });
}
//連接資料庫，執行以下代碼刪除文檔
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行刪除文檔操作，調用自訂方法
    deleteData(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});

//------------------------------------------------------------------------------------------

//2. collection. deleteMany()方法
//15.3  應用與操作
//15.3.2  刪除文檔
//---2. collection. deleteMany()方法
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//定義函數運算式，用於操作資料庫並返回結果
//定義刪除資料的方法
var deleteData = function(db, callback) {  
    //獲得指定的集合 
    var collection = db.collection("Members");
    //定義要刪除所有文檔
    var  where={};
    collection.deleteMany(where,function(err, result) {         
        callback(result);
    });
}
//連接資料庫，執行以下代碼刪除文檔
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行刪除文檔操作，調用自訂方法
    deleteData(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});
  
//------------------------------------------------------------------------------------------
 
//15.3.3 修改文檔
//1. collection.updateOne()
//15.3  應用與操作
//15.3.3  修改文檔 
//---1. collection.updateOne()
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//定義修改資料的方式，用於操作資料庫並返回結果
var updateData = function(db, callback) {  
    //獲得指定的集合 
    var collection = db.collection("Members");
    //要修改資料的條件，"Name"為"Jerry Deng"的文檔將被修改
    var  where={"Name":"Jerry Deng"};
    collection.updateOne(where,{"$set":{"Tel":"13800138444"}},function(err, result) { 
        //如果存在錯誤
        if(err)
        {
            console.log('Error:'+ err);
        }
        //調用傳入的回檔方法，將操作結果返回
        callback(err,result);
    });
}
//連接資料庫，執行以下代碼修改文檔
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行修改文檔操作，調用自訂方法
    updateData(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});

//------------------------------------------------------------------------------------------

//2. replaceOne()
//15.3  應用與操作
//15.3.3  修改文檔 
//---2. replaceOne()
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//定義修改資料的方式，用於操作資料庫並返回結果
var updateData = function(db, callback) {  
    //獲得指定的集合 
    var collection = db.collection("Members");
    var where={"Name":"Mary Huang"};    
    collection.replaceOne(where,{"Tel":"13800138555"},{"upsert":true},
    function(err, result) 
    { 
        if(err) {
            console.log('Error:'+ err);    
        }
        callback(err,result);
    });
}
//連接資料庫，執行以下代碼修改文檔
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行修改文檔操作，調用自訂方法
    updateData(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});

