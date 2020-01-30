15.4.1 限制查詢結果集大小
1. 一些常用的查詢準則設置
//15.4  查詢文檔
//15.4.1  限制查詢結果集大小
//---1. 一些常用的查詢準則設置
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//定義修改資料的方式，用於操作資料庫並返回結果
var findData = function(db, callback) {  
    //獲得指定的集合 
    var collection = db.collection("Members");
    
//（1）單個查詢準則的用法
    //不限制條件的查找
    var  where={};
    
//（2）mongodb驅動程式提供的文檔篩選條件及對應的T-SQL指令
    
    //查找"Name"為"Mary"的文檔
    //var  where ={ "Name":"Mary"};
    
    //要查詢的值隸屬於某個列表範圍中
    //var  where={"Name": {"$in": ["Mary", " Lucy"]}}
    
    //設置多個查詢準則
    //var  where={ "Name":"Mary","TEL":"13800138555"};
    
    //使用"or"語句串聯查詢準則
    //var  where ={$or:[{"Name":"Mary"},{"Tel":"13800138555"}]};
    
    //Age小於等於25
    //var  where ={age:{"$lte":25}};
    
//（3）查找性別為"男"的人員數量和明細
    //var  where={"Gender":"Male"}; 
    
    collection.find(where).toArray(function(err, result) { 
        //如果存在錯誤
        if(err)
        {
            console.log('Error:'+ err); 
        } 
        //調用傳入的回檔方法，將操作結果返回
        callback(err,result);
    });
}
//連接資料庫，執行以下代碼查詢文檔
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行查詢文檔操作，調用自訂方法
    findData(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});

-------------------------------------------------------------------------------------

2. 統計文檔數量與排序
//15.4  查詢文檔
//15.4.1  限制查詢結果集大小
//---2. 統計文檔數量與排序
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//定義修改資料的方式，用於操作資料庫並返回結果
var findData = function(db, callback) {  
    //獲得指定的集合 
    var collection = db.collection("Members");
    
    //不限制條件的查找
    var  where={};
    
    collection.find(where).sort("Tel",1).toArray(function(err, result){
        if(err){
            console.log('Error:'+ err);
        } 
        callback(err,result);
    });
}
//連接資料庫，執行以下代碼查詢文檔
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行查詢文檔操作，調用自訂方法
    findData(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});

-------------------------------------------------------------------------------------
   
15.4.2 限制查詢欄位
//15.4  查詢文檔
//15.4.2  限制查詢欄位
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
var findData = function(db, callback) {  
    var collection = db.collection('Members');
    var  where={"Gender":"Male"};
    //設置要顯示的欄位
    var fields={fields:{"Name":1,"Gender":1}};
    collection.find(where).count(function(err,rCount){
        callback(rCount);
    });
    collection.find(where,fields).toArray(function(err, result) {      
        if(err)        
        {
            console.log('Error:'+ err);            
        } 
        //調用傳入的回檔方法，將操作結果返回
        callback(err,result);
    });
}
//連接資料庫，執行以下代碼查詢文檔
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行查詢文檔操作，調用自訂方法
    findData(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});
 
-------------------------------------------------------------------------------------
 
15.4.3 查詢準則使用
//15.4  查詢文檔
//15.4.3  查詢準則使用
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
var findData = function(db, callback) {  
    var collection = db.collection('Members');
    //查詢準則
    var  where={"$and":[{"Gender":"Male"}, {"Location.Province":"GuangDong"}]};
    //設置要顯示的欄位
    var fields={fields:{"Name":1, "Gender":1,"Location":1}};
    collection.find(where,fields).count(function(err,rCount){
        callback(rCount);
    });
    collection.find(where,fields).toArray(function(err, result) { 
        if(err)        
        {
            console.log('Error:'+ err);
        } 
        //調用傳入的回檔方法，將操作結果返回
        callback(err,result);
    });
}
//連接資料庫，執行以下代碼查詢文檔
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行查詢文檔操作，調用自訂方法
    findData(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});
 
-------------------------------------------------------------------------------------
 
15.4.4 將查詢結果分頁
//15.4  查詢文檔
//15.4.4  將查詢結果分頁
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//定義查詢分頁函數
var findData_Pager = function(pageIndex,pageSize,db, callback) {  
    var collection = db.collection('Members');
    //要查詢資料的條件："Gender"為"Male"的用戶
    var  where={"Gender":"Male"};
    //設置要顯示的欄位
    var fields={fields:{ "_id":0,"Name":1,"Gender":1, "Tel":1}};
    collection.find(where).count(function(err,rCount){
        console.log("當前頁："+pageIndex +" 每頁："+pageSize+" 總計："+rCount);
    });
    collection.find(where,fields).limit(pageSize).skip((pageIndex-1)*pageSize).sort({"_id":1}).toArray(function(err, result) { 
        //如果存在錯誤
        if(err)
        {
            console.log('Error:'+ err);
        } 
        //調用傳入的回檔方法，將操作結果返回
        callback(err,result);
    });
}
//執行查詢分頁
MongoClient.connect(Urls,function(err, client) {
    
    const db = client.db(dbName);
    //執行查詢分頁資料操作，調用自訂方法
    findData_Pager(1,4,db, function(err,result) {
        //顯示結果
        console.log(result); 
    });
    findData_Pager(2,4,db, function(err,result) {
        //顯示結果
        console.log(result); 
    });
    findData_Pager(3,4,db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});
  
-------------------------------------------------------------------------------------

15.4.5 使用聚合方法查詢文檔
1．範例一
//15.4  查詢文檔
//15.4.5  使用聚合方法查詢文檔
//---1．範例一
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//定義聚合操作方法
var aggregateFind = function(db, callback) {  
    //獲得指定的集合 
    var collection = db.collection('Members');
    //設置要執行的聚合參數
    var pipeline=[
        {"$project":{"_id":0,"Name":1,"Province":["$Location.Province"]}},
        {"$unwind":"$Province"},
        {"$group":{"_id":"$Province","Members":{"$addToSet":"$Name"},"Num":{"$sum":1}}},
        {"$project":{"_id":0,"Province":"$_id","Num":1,"Members":1}},
        {"$limit":3}         
    ];
    collection.aggregate(pipeline).toArray(function(err, result) { 
        if(err)
        {
            console.log('Error:'+ err);
        } 
        //調用傳入的回檔方法，將操作結果返回
        callback(err,result);
    });
}
//連接資料庫，執行聚合操作方法
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行聚合操作方法，調用自訂方法
    aggregateFind(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});

-------------------------------------------------------------------------------------

2．範例二
//15.4  查詢文檔
//15.4.5  使用聚合方法查詢文檔
//---2．範例二
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//定義聚合操作方法
var aggregateFind = function(db, callback) {  
    //獲得指定的集合 
    var collection = db.collection('Members');
    //設置要執行的聚合參數
    var pipeline=[
        {"$project":{"_id":0,"Name":1,"Province":["$Location.Province"]}},
        {"$unwind":"$Province"},
        {"$group":{"_id":"$Province","Members":{"$addToSet":"$Name"},"Num":{"$sum":1}}},
        {"$project":{"_id":0,"Province":"$_id","Num":1,"Members":1}},
        {"$sort":{"Num":-1}},
        {"$limit":3}    
    ]; 
    collection.aggregate(pipeline).toArray(function(err, result) { 
        if(err)
        {
            console.log('Error:'+ err);
        } 
        //調用傳入的回檔方法，將操作結果返回
        callback(err,result);
    });
}
//連接資料庫，執行聚合操作方法
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行聚合操作方法，調用自訂方法
    aggregateFind(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});
  
-------------------------------------------------------------------------------------

15.4.6 用索引進行查詢
//15.4  查詢文檔
//15.4.6  用索引進行查詢
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//定義索引應用方法
var indexTest = function(db, callback) {  
    var collection = db.collection('Members');
    
    //（1）普通索引
    //db.collection.createIndex({"name": 1})
    
    //（2）複合索引
    //db.collection.createIndex({"name": 1,"Gender": 1})
    
    //（3）後臺作業
    //db.collecetion.createIndex({"name": 1}, {"background": true})
    
    //（4）唯一索引
    //db.collection.createIndex({"name": 1}, {"unique": true})
        
    //（5）查看索引
    //調用顯示索引資訊的方法
    collection.indexInformation(function(err, result) { 
        if(err){
            console.log('Error:'+ err);
        } 
        callback(err,result); 
    });
        
    //（6）添加索引
    collection.createIndex({"Name":1},function(err, result) { 
        if(err){
            console.log('Error:'+ err);
        } 
        //調用傳入的回檔方法，將操作結果返回
        callback(err,result);
    })
    
    //（7）刪除索引
    collection.dropIndex("Name_1",function(err, result){
        if(err){
                console.log('Error:'+ err); 
            } 
            callback(err,result);
    });
}
//連接資料庫，執行索引應用方法
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行索引應用方法，調用自訂方法
    indexTest(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});

