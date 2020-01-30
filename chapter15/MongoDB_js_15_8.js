//15.8  操作MongoDB GridFS
//引入mongodb驅動程式
var MongoClient = require("mongodb").MongoClient; 
//引入斷言模組
var assert = require("assert"); 
//將URL放在連接池中
var Urls = "mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017";  
 //定義資料庫名稱
const dbName = "E-commerce";
 
//定義Gridfs方法
var Gridfsfunction = function(db, callback) {  
     
    var mongoose = require('mongoose');
    var Schema = mongoose.Schema;
    mongoose.connect('mongodb://<mongodb_user>:<mongopdb_pwd>@127.0.0.1:27017/E-commerce');
    var conn = mongoose.connection; 
    var fs = require('fs');
    var Grid = require('gridfs-stream');
    Grid.mongo = mongoose.mongo;
    conn.once('open', function () {
        console.log('open');
        var gfs = Grid(conn.db);
        
        //在測試以下操作時，可將其餘操作批註掉，以驗證各自的效果。
        
        // 寫文件
        var writestream = gfs.createWriteStream({ 
            filename: 'mongo_file.txt'
        });
        fs.createReadStream('./mongo_file.txt').pipe(writestream);
        writestream.on('close', function (file) {
            console.log(file.filename + ' Written To DB');
        });
        
        // 讀取文件
        var fs_write_stream = fs.createWriteStream('./write.txt'); 
        var readstream = gfs.createReadStream({
            filename: 'mongo_file.txt'
        });
        readstream.pipe(fs_write_stream);
        fs_write_stream.on('close', function () {
            console.log('file has been written fully!');
        });
        
        //判斷檔是否存在
        var options = {filename : 'mongo_file.txt'}; // 使用"_id"欄位也可以
        gfs.exist(options, function (err, found) {
            if (err) return handleError(err);
            found ? console.log('File exists') : console.log('File does not exist');
        });
        
        // 獲取檔的基礎資訊
        gfs.files.find({ filename: 'mongo_file.txt' }).toArray(function (err, files) {
            if (err) {
                throw (err);
            }
            console.log(files);
        });
        
        // 根據檔案名稱刪除檔
        gfs.remove({filename: 'mongo_file.txt'}, function (err) {
            if (err) return handleError(err);
            console.log('success');
        });
        // 根據fs.files._id刪除檔
        gfs.remove({_id : '548d91dce08d1a082a7e6d96'}, function (err) {
            if (err) return handleError(err);
            console.log('success');
        });
    });
}
//連接資料庫，執行Gridfs方法
MongoClient.connect(Urls,function(err, client) {
    assert.equal(null, err);
    const db = client.db(dbName);
    //執行Gridfs方法，調用自訂方法
    Gridfsfunction(db, function(err,result) {
        //顯示結果
        console.log(result);
    });
    client.close();
});

