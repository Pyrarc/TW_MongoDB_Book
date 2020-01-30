#!/usr/bin/python
#This Python file uses the following encoding:utf-8
          
#14.2  建立連接與斷開連接
#-1. 建立連接
          
#導入pymongo包
import pymongo
              
#連接MongoDB字串
mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
        
#建立與MongoDB的連接，並將此連接賦值給自訂的“mongo”變數
mongo=pymongo.MongoClient(mongodb_server_uri)
        
#定位到E-commerce資料庫
db=mongo["E-commerce"]
       
#定位到Members集合
collection=db.get_collection("Members")
          
#讀取Members集合中的記錄數並列印
print("Number of Documents: "+str(collection.find().count()))
         
#關閉MongoDB連接
mongo.close()

