#!/usr/bin/python
#This Python file uses the following encoding:utf-8
#14.6  批量處理資料
#-2. Python的collection.bulk_write()方法
  
import pymongo
from pymongo import InsertOne, DeleteMany, ReplaceOne, UpdateOne,UpdateMany
    
#定義一個批量操作的類
class mongodb_bulk(object):
#定義初始化執行方法，包含建立MongoDB的連接、讀取Members資料庫
    def __init__(self):
        mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
        mongo = pymongo.MongoClient(mongodb_server_uri)
        db = mongo["E-commerce"]
        self.collection = db["Members"]
  
    #定義批量執行某請求的方法
    def bulk_process(self,request):
        result =self.collection.bulk_write(request)
        #result =self.collection.bulk_write(request,ordered=False)
        return result
  
    #定義基於條件獲取文檔的方法
    def get_docs(self,query):
        cursor =self.collection.find(query)
        return cursor
  
if __name__ == '__main__':
  
#創建若干個文檔
    new_doc1={"Name" : "Tony",
             "Gender" : "Male",
             "Tel" : "18800000003",
             "CustomerSysNo" : 11339491,
             "Location" : { "City" : "Keelung", "Dist" : "Xinyi" },
              "Type":"bulk"
             }

    new_doc2={}
    new_doc2["Name"]="Cindy"
    new_doc2["Gender"]="Female"
    new_doc2["Tel"]="18800000004"
    new_doc2["CustomerSysNo"]="32245233"
    new_doc2["Location"]={ "City" : "Kaohsiung", "Dist" : "Zuoying"}
    new_doc2["Type"]="bulk"

    new_doc3={"Name" : "Lucy",
             "Gender" : "Female",
             "Tel" : "18800000001",
             "CustomerSysNo" : 11339491,
             "Location" : { "City" : "Kaohsiung", "Dist" : "Zuoying" },
             "Type":"bulk"
             }

    new_doc4={"Name" : "Elsa",
             "Gender" : "Female",
             "Tel" : "18800000009",
             "CustomerSysNo" : 23456789,
             "Location" : { "City" : "Taipei", "Dist" : "Da-an" },
             "Type":"bulk",
             "Age":20
             }
  
    #定義批量操作請求，先插入3個文檔，再進行更新和替換
    request=[
        InsertOne(new_doc1),
        InsertOne(new_doc2),
        InsertOne(new_doc3),
        UpdateOne({'Name': "Lucy"}, {'$set': {'Tel': '18800000010'}}),
        UpdateOne({'Name': "Anna"}, {'$inc': {'Age': 1}}, upsert=True),
        UpdateOne({'Name': "Anna"}, {'$set': {'Type': 'bulk'}}),
        UpdateMany({'Type': "bulk"}, {'$inc': {'Age': 10}}),
        ReplaceOne({"Name":"Tony"},new_doc4),
            ]
  
    #獲取mongodb_bulk類的對象
    bulk=mongodb_bulk()
  
    #執行批量操作
    result=bulk.bulk_process(request)
   
    #列印批量操作結果
    print(result.bulk_api_result)
   
    #列印Type為bulk的文檔
    print("----------------------------------------------------")
    for doc in bulk.get_docs({"Type":"bulk"}):
        print(doc)

