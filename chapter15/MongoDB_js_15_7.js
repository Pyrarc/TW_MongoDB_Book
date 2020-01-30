//15.7  創建文檔關聯查詢
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//定義關聯查詢方法
var lookupFind = function(db, callback) {  
    //獲得指定的集合 
    var collection = db.collection('Members');
    var joinSearch=[
       
        {"$lookup":{from: "Carts",
                    localField: 'CustomerSysNo',
                    foreignField: 'CustomerSysNo',  
                    as: 'MyCarts'
                    }
        },
        {"$match":{"Name":"Oughl Luo"}},
        {"$project":{"_id":0,"Name":1,"MyCarts":1}}
    ];
    collection.aggregate(joinSearch).toArray(function(err, results) { 
        for (var i in results)
            {
                console.log(results[i].Name) ;
                console.log(results[i].MyCarts.length) ;
                console.log(results[i].MyCarts);
            }
    });
}
//連接資料庫，執行關聯查詢方法
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行關聯查詢方法，調用自訂方法
    lookupFind(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});

