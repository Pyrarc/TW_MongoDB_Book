#14.3.1  新增文檔
#!/usr/bin/python
#This Python file uses the following encoding:utf-8
      
# 14.3  應用與操作
#---1. 用collection.insert()方法新增文檔------------
       
import pymongo
from bson.objectid import ObjectId
from pprint import pprint
         
mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
mongo=pymongo.MongoClient(mongodb_server_uri)
        
db=mongo["E-commerce"]
collection=db["Members"]
      
#建立文檔內容
new_doc={"Name" : "Nina",
         "Gender" : "Female",
         "Tel" : "18800000001",
         "CustomerSysNo" : 11339491,
         "Location" : { "City" : "Kaohsiung", "Dist" : " Zuoying" }
         }
         
#將文檔插入MongoDB
collection.insert(new_doc)
print("Number of Documents: "+str(collection.find().count()))

#--------------------------------------------------------------------------------- 
 
#!/usr/bin/python
#This Python file uses the following encoding:utf-8
        
# 14.3  應用與操作
#---2. 用字典方式新增文檔---------------------------
         
import pymongo
mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
mongo=pymongo.MongoClient(mongodb_server_uri)
          
db=mongo["E-commerce"]
collection=db["Members"]
       
#建立字典格式文檔
new_doc={}
          
#指定該文檔字典的內容
new_doc["Name"]="Lucy"
new_doc["Gender"]="Female"
new_doc["Tel"]="18800000002"
new_doc["CustomerSysNo"]="33442233"
new_ doc["Location"]={"City" : "Taipei", "Dist" : "Wanhua"}
        
collection.insert(new_doc)
doc=collection.find_one({"Name":"Lucy"})
    
print(doc)
print("Number of Documents: "+str(collection.find().count()))

#--------------------------------------------------------------------------------- 
 
#!/usr/bin/python
#This Python file uses the following encoding:utf-8
    
# 14.3.1  新增文檔-----
#-3. 批量新增文檔
       
import pymongo
     
mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
mongo=pymongo.MongoClient(mongodb_server_uri)
       
db=mongo["E-commerce"]
collection=db["Members"]
     
#創建第1個文檔
new_doc1={"Name" : "Tony",
         "Gender" : "Male",
         "Tel" : "18800000003",
         "CustomerSysNo" : 11339491,
         "Location" : { "City" : "Keelung", "Dist" : "Xinyi" }
         }
     
#創建第2個文檔
new_doc2={}
new_doc2["Name"]="Cindy"
new_doc2["Gender"]="Female"
new_doc2["Tel"]="18800000004"
new_doc2["CustomerSysNo"]="32245233"
new_doc2["Location"]={"City" : "Kaohsiung", "Dist" : "Zuoying"}
     
#同時插入兩個文檔
result=collection.insert_many([new_doc1,new_doc2])       
print(result.inserted_ids)
        
new_doc_count=collection.find({"Name":{"$in":["Tony","Cindy"]}}).count()
print("Number of Inserted Documents: "+str(new_doc_count))
print("Number of Total Documents: "+str(collection.find().count()))

#--------------------------------------------------------------------------------- 
 
#!/usr/bin/python
#This Python file uses the following encoding:utf-8
     
# 14.3.1  新增文檔-----
#-4. 用collection.save()方法新增文檔
       
import pymongo
from bson.objectid import ObjectId
from pprint import pprint
               
mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
mongo=pymongo.MongoClient(mongodb_server_uri)
db=mongo["E-commerce"]
collection=db["Members"]
         
#新增文檔
new_doc2={}
new_doc2["Name"]="Anna"
new_doc2["Gender"]="Female"
new_doc2["Tel"]="18800000005"
new_doc2["CustomerSysNo"]="00001111"
new_doc2["Location"]={"City" : "Kaohsiung", "Dist" : "Zuoying"}
rst=collection.save(new_doc2)
         
doc=collection.find_one({"Name":"Anna"})
         
#列印該文檔
print("Doc before modify:")
pprint(doc)
           
print(rst)
#修改文檔中"Tel"欄位的值，並用save()進行保存
new_doc2["_id"]=rst
new_doc2["Tel"]="18800000006"
rst=collection.save(new_doc2)
             
doc=collection.find_one({"Name":"Anna"})
print("Doc after modify:")
pprint(doc)
 
--------------------------------------------------------------------------------- 
      
#14.3.2 刪除文檔

#!/usr/bin/python
#This Python file uses the following encoding:utf-8

#14.3.2 刪除文檔
#1. 刪除單個文檔              
import pymongo
mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
mongo=pymongo.MongoClient(mongodb_server_uri)
       
db=mongo["E-commerce"]
collection=db["Members"]
         
#設置查詢文檔的條件
query={"Name":"Cindy"}
doc=collection.find_one(query)
print("The Document of 'Cindy':")
print(doc)
          
#執行刪除文檔
collection.delete_many(query)
doc=collection.find_one(query)
          
print("The Document of 'Cindy' after deleted:")
print(doc)

#--------------------------------------------------------------------------------- 
 
#!/usr/bin/python
#This Python file uses the following encoding:utf-8

#14.3.2 刪除文檔
#2. 刪除所有文檔         
           
import pymongo
mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
mongo=pymongo.MongoClient(mongodb_server_uri)
        
db=mongo["E-commerce"]
collection=db["Members"]
          
result = collection.delete_many({})

#--------------------------------------------------------------------------------- 
 
#14.3.3 修改文檔
#!/usr/bin/python
#This Python file uses the following encoding:utf-8
      
#14.3.3  修改文檔
#-1. 用collection.update_one()方法更新文檔
           
import pymongo
from pprint import pprint
mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
mongo=pymongo.MongoClient(mongodb_server_uri)
            
db=mongo["E-commerce"]
collection=db["Members"]
            
#將"Name"為"Anna"的"Tel"改為"18800000002"
rst=collection.update_one({"Name":"Anna"},{"$set":{"Tel":"18800000002"}})
            
#列印出符合條件的文檔數及被修改的文檔數
print("matched_count: "+str(rst.matched_count))
print("modified_count: "+str(rst.modified_count))
             
doc=collection.find_one({"Name":"Anna"})
print("Doc after modify:")
pprint(doc)

#--------------------------------------------------------------------------------- 
 
#!/usr/bin/python
#This Python file uses the following encoding:utf-8
         
#14.3.3  修改文檔
#-2. 用collection.replace_one()方法更新文檔
       
import pymongo
from pprint import pprint
mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
mongo=pymongo.MongoClient(mongodb_server_uri)
         
db=mongo["E-commerce"]
collection=db["Members"]
             
#直接找到對應文檔進行替換修改
rst=collection.replace_one({"Name":"Anna"},{"Tel":"18800000002"})
print("matched_count: "+str(rst.matched_count))
print("modified_count: "+str(rst.modified_count))
            
doc=collection.find_one({"Name":"Anna"})
print("Doc after modify:")
pprint(doc)

