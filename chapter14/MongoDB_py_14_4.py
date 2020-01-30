4.4.1  限制查詢結果集大小
#!/usr/bin/python
#This Python file uses the following encoding:utf-8
     
#14.4.1  限制查詢結果集大小
#-2. 用完整代碼來限制查詢的MongoDB資料
      
import pymongo
from pprint import pprint
       
mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
mongo=pymongo.MongoClient(mongodb_server_uri)
db=mongo["E-commerce"]
collection=db["Members"]
          
#（1）只查出"Gender"欄位為"Male"的文檔。
     
#查找所有"Gender"欄位為"Male"的文檔
cursor=collection.find({"Gender" : "Male"})
for doc in cursor:
    pprint("Name:"+doc["Name"]+" Gender:"+doc["Gender"]+" Tel:"+doc["Tel"])
    
      
#（2）查詢文檔數量並將文檔按照特定欄位排序。
        
#查找所有"Gender"欄位為"Male"的文檔
cursor=collection.find({"Gender" : "Male"})
     
#將查找結果按首字母正序排列
cursor.sort("Name", pymongo.ASCENDING)
print(cursor.count())
        
for doc in cursor:
    pprint("Name:"+doc["Name"]+" Gender:"+doc["Gender"]+" Tel:"+doc["Tel"])

--------------------------------------------------------------------------------------

           
14.4.2 限制查詢返回的欄位
#!/usr/bin/python
#This Python file uses the following encoding:utf-8
        
#14.4.2  限制查詢返回的欄位
         
import pymongo
from pprint import pprint
        
mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
mongo=pymongo.MongoClient(mongodb_server_uri)
        
db=mongo["E-commerce"]
collection=db["Members"]
        
#指定要輸出的欄位
fields={"Name":True,"Gender":True,"Tel":True}
       
#設置讓"_id"欄位不顯示
fields={"Name":True,"Gender": True,"Tel": True,"_id":False}
         
#將欲輸出的欄位應用到查詢語句中
cursor=collection.find({"Gender" : "Male"},fields)
cursor.sort("Name", pymongo.ASCENDING)
print(cursor.count())
           
for doc in cursor:
       print(doc)
   

--------------------------------------------------------------------------------------

14.4.3 用複雜條件進行查詢
#!/usr/bin/python
#This Python file uses the following encoding:utf-8
        
#14.4.3  用複雜條件進行查詢
import pymongo
from pprint import pprint
        
mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
mongo=pymongo.MongoClient(mongodb_server_uri)
           
db=mongo["E-commerce"]
collection=db["Members"]
fields={"_id":False,"Name":True,"Gender":True,"Tel":True,"Location":True}
               
#設定組合條件
query={"Gender":"Female","$or":[{"Location.Province":"ShangHai"},{"Location.City":"ShangHai"}]}
cursor=collection.find(query,fields)
cursor.sort("Name", pymongo.ASCENDING)
print(cursor.count())
for doc in cursor:
       print(doc)

--------------------------------------------------------------------------------------

14.4.4 將查詢結果分頁顯示
#!/usr/bin/python
#This Python file uses the following encoding:utf-8
     
#14.4.4  將查詢結果分頁顯示
#-2. 分頁查詢的完整代碼
      
import pymongo
     
def get_documents(page_size,current_page):
    mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
    mongo = pymongo.MongoClient(mongodb_server_uri)
      
    db = mongo["E-commerce"]
    collection = db["Members"]
    fields = {"_id": False, "Name": True, "Gender": True, "Tel": True}
    query = {}
        
    #用find()方法，並帶上query和fields參數
    cursor = collection.find(query, fields)
      
    #將find()讀取的結果按照"Name"欄位的值順序排列
    cursor.sort("Name", pymongo.ASCENDING)
     
    #限制讀取文檔的數量
    cursor.limit(page_size)
      
    #設置讀取文檔時跳過的文檔數量
    sub_cursor=cursor.skip((current_page-1) * page_size)
    return sub_cursor
      
#創建一個方法，用來獲取Members集合中的記錄數
def get_count():
    mongodb_server_uri="mongodb://user:pwd@127.0.0.1:27017"
    mongo = pymongo.MongoClient(mongodb_server_uri)
     
    db = mongo["E-commerce"]
    collection = db["Members"]
    query = {}
    return collection.find(query).count()
  
#設定每頁顯示的文檔數
page_size=3
       
#獲取Members集合中的總記錄數
doc_numbers=get_count()

--------------------------------------------------------------------------------------

14.4.5 用聚合方法查詢文檔
#!/usr/bin/python
#This Python file uses the following encoding:utf-8
      
#14.4.5  用聚合方法查詢文檔
import pymongo
     
def get_group():
    mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
    mongo = pymongo.MongoClient(mongodb_server_uri)
    
    db = mongo["E-commerce"]
    collection = db["Members"]
    
    #設定aggregate的聚合管道
    pipeline=[
        {"$project":{"_id":0,"Name":1,"Province":"$Location.Province"}},
        {"$group":{"_id":"$Province","Members":{"$addToSet":"$Name"},"Num":{"$sum":1}}},
        {"$project":{"_id":0,"Province":"$_id","Members":1,"Num":1}},
        {"$sort":{"Num":-1}},
        {"$limit":3}
    ]
    
    #執行聚合管道計算
    cursor=collection.aggregate(pipeline)
    return cursor
    
#列印計算結果
for doc in get_group():
    print(doc)
 
--------------------------------------------------------------------------------------

14.4.6 用索引查詢
#!/usr/bin/python
#This Python file uses the following encoding:utf-8
     
#14.4.6  用索引查詢     
#（5）查看索引
      
import pymongo
    
def get_Indexes():
    mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
    mongo = pymongo.MongoClient(mongodb_server_uri)
        
    db = mongo["E-commerce"]
    collection = db["Members"]
     
    #讀取Members表中已有的索引
    cursor=collection.index_information()
    mongo.close()
    return cursor
        
for doc in get_Indexes():
    print(doc)

--------------------------------------------------------------------------------------

#!/usr/bin/python
#This Python file uses the following encoding:utf-8
   
#14.4.6  用索引查詢
#（6）添加索引
      
import pymongo
  
def get_Indexes():
    mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
    mongo = pymongo.MongoClient(mongodb_server_uri)
      
    db = mongo["E-commerce"]
    collection = db["Members"]
      
    #創建"Name"欄位按順序排列的索引，並且在後臺執行
    collection.create_index([("Name",pymongo.ASCENDING)],background=True)
    cursor=collection.index_information()
    mongo.close()
    return cursor
          
for doc in get_Indexes():
    print(doc)

--------------------------------------------------------------------------------------

#!/usr/bin/python
#This Python file uses the following encoding:utf-8
     
#14.4.6  用索引查詢
#（7）刪除索引
   
import pymongo
  
def get_Indexes():
    mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
    mongo = pymongo.MongoClient(mongodb_server_uri)
    db = mongo["E-commerce"]
    collection = db["Members"]
        
    #刪除"Name"欄位的索引
    collection.drop_index([("Name",pymongo.ASCENDING)])
    cursor=collection.index_information()
    mongo.close()
    return cursor
         
for doc in get_Indexes():
    print(doc)

--------------------------------------------------------------------------------------

#!/usr/bin/python
#This Python file uses the following encoding:utf-8
       
#14.4.6  用索引查詢
#（8）文本索引。
         
import pymongo
from bson.objectid import ObjectId
from pprint import pprint
         
def set_Indexes():
    mongodb_server_uri = "mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
    mongo = pymongo.MongoClient(mongodb_server_uri)
     
    db = mongo["E-commerce"]
    collection = db["Members"]
    
    #建立文本索引    
    collection.create_index([("Location.Province",pymongo.TEXT),("Location.City",pymongo.TEXT)])
    
    cursor=collection.index_information()
    mongo.close()
    return cursor
     
def text_searching(str):
    mongodb_server_uri = "mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
    mongo = pymongo.MongoClient(mongodb_server_uri)
    db = mongo["E-commerce"]
    collection = db["Members"]
    fields={"Name":1,"Location":1,"_id":0}
     
    #用文本索引查詢
    query = { "$text": { "$search": str }}
    cursor = collection.find(query,fields)
    print(cursor.count())
    mongo.close()
    return cursor
  
for doc in set_Indexes():
    print(doc)
  
#設置要查詢的文本，多個查詢文本可以用空格隔開
query_str="GuangXi ShenZhen GuangZhou"
for doc in text_searching(query_str):
    print(doc)

