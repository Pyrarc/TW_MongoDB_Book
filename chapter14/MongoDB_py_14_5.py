#!/usr/bin/python
#This Python file uses the following encoding:utf-8
   
#14.5  使用規則運算式
#2. 比較規則運算式兩種使用方式
     
import pymongo
#導入re包
import re
      
#創建一個方法，以返回基於某查詢準則的"Members"文檔
def get_docs(query):
    mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
    mongo = pymongo.MongoClient(mongodb_server_uri)
   
    db = mongo["E-commerce"]
    collection = db["Members"]
     
    fields={"Name":1,"Tel":1,"Gender":1,"_id":0}
    cursor =collection.find(query,fields)
    mongo.close()
    return cursor
  
#（1）案例一。
#設定一個規則運算式，指定以字母"c"開頭，IGNORECASE表示不區分大小寫
regx=re.compile('^c',re.IGNORECASE)
query1 = {"Name":regx}
     
#另一種方式設置規則運算式，同樣指定以字母"c"開頭，且不區分大小寫
query2 = {"Name":{"$regex":r"^c","$options":"i"}}
       
for doc in get_docs(query1):
    print(doc)
      
print("----------------------------------------------------")
for doc in get_docs(query2):
    print(doc)
        
     
#（2）案例二。
#設定一個規則運算式，指定包含字母"lu"，且不區分大小寫
regx=re.compile('lu',re.IGNORECASE)
query1 = {"Name":regx}
       
#用另一種方式設定規則運算式，指定以字母"lu"結尾，且不區分大小寫
query2 = {"Name":{"$regex":r"lu$","$options":"i"}}
       
for doc in get_docs(query1):
    print(doc)
          
print("----------------------------------------------------")
for doc in get_docs(query2):
    print(doc)

