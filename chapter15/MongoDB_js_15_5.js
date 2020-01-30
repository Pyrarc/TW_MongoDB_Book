//15.5  使用規則運算式
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
////定義規則運算式方法
var findData_Regax = function(db, callback) {  
    //獲得指定的集合 
    var collection = db.collection('Members');
    var match1 = /.*y\b/i;  
    var query={"Name":match1}
    
    //設置要顯示的欄位
    var fields={fields:{ "_id":0,"Name":1,"Location":1}};
    collection.find(query).count(function(err,rCount){
        callback(rCount);
    });
    collection.find(query,fields).toArray(function(err, result) { 
        if(err){
            console.log('Error:'+ err);
        }
        callback(err,result);
    });
}
//連接資料庫，執行規則運算式方法
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行規則運算式方法，調用自訂方法
    findData_Regax(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});

