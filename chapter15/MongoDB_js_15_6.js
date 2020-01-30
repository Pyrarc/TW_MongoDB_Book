//15.6  批量處理資料
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//定義批量處理資料方法
var blukWriteTest = function(db, callback) {  
    //獲得指定的集合 
     var collection = db.collection('Members');
     var new_doc1={"Name" : "Tony",
        "Gender" : "Male",
        "Tel" : "13800138011",
        "CustomerSysNo" : 11339491,
        "Location" : { "City" : "Keelung", "Dist" : "Xinyi" },
        "Type":"bulk"
        };

	var new_doc2={}
        new_doc2["Name"]="Susan"
        new_doc2["Gender"]="Female"
        new_doc2["Tel"]="13800138012"
        new_doc2["CustomerSysNo"]="32245233"
        new_doc2["Location"]={"City":"Keelung","Dist":"Xinyi"}
        new_doc2["Type"]="bulk"
    
      var new_doc3={"Name" : "Lucy",
             "Gender" : "Female",
             "Tel" : "13800138013",
             "CustomerSysNo" : 11339491,
             "Location" : { "City" : "Taipei", "Dist" : "Wanhua" },
             "Type":"bulk"
             };
	
      var new_doc4={"Name" : "Elsa",
             "Gender" : "Female",
             "Tel" : "13800138014",
             "CustomerSysNo" : 23456789,
             "Location" : { "City" : "Taipei", "Dist" : "Da-an" },             
             "Type":"bulk",
             "Age":20
             };

    
     var bulkDocs=[
                {insertOne:{document:new_doc1}},
                {insertOne:{document:new_doc2}},
                {insertOne:{document:new_doc3}},
                {updateOne:{filter: {'Name': 'Jerry Deng'}, update: {$set: {'Tel': '13800138999'}}, upsert:true } },
                {updateMany:{filter: {'Tel': '18000000000'}, update: {$set: {'Tel': '13800138000'}}, upsert:true } },
                {replaceOne: { filter: {'Name': 'Sam Liu'}, replacement:new_doc4, upsert:true}},                
                {deleteOne: { filter: {'Name': 'Mary Huang'} } }                
            ];
    collection.bulkWrite(bulkDocs, function (err, result) {
        if(err){
            console.log('Error:'+ err);
        }
        callback(err,result);
    });
} 
//連接資料庫，執行批量處理資料方法
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行批量處理資料方法，調用自訂方法
    blukWriteTest(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});

