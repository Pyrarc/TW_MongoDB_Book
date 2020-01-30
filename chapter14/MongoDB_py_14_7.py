#!/usr/bin/python
#This Python file uses the following encoding:utf-8
      
#14.7  創建文檔關聯查詢
     
import pymongo
  
def get_cart():
    mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
    mongo = pymongo.MongoClient(mongodb_server_uri)
   
    db = mongo["E-commerce"]
    collection = db["Carts"]
   
    #建立aggregate管道計算
    pipeline=[
         {"$project":{"_id":0,"CustomerSysNo":1,"Quantity":1,"Price":"$product.Price","Product":"$product.ProductName"}}
        ,{
          "$lookup": {
             "from": "Members",
             "localField": "CustomerSysNo",
             "foreignField": "CustomerSysNo",
             "as": "Members"
          }
         },{
          "$replaceRoot": { "newRoot": { "$mergeObjects": [ { "$arrayElemAt": [ "$Members", 0 ] }, "$$ROOT" ] } }
         }
        ,{"$project": {"_id": 0, "Name": 1, "Quantity": 1, "Amount": {"$multiply": ["$Price", "$Quantity"]},"Product": 1, "Price": 1}}
        ,{"$project": {"Name": 1, "Quantity": 1, "Amount": 1, "Product": ["$Product", "$Quantity", "$Price", "$Amount"]}}
        ,{"$group":{"_id":"$Name","Product":{"$addToSet":"$Product"},"Quantity":{"$sum":"$Quantity"},"Amount":{"$sum":"$Amount"}}}
        ,{"$project":{"Name":"$_id","_id":0,"Product":1,"Quantity":1,"Amount":1}}
        ,{"$sort": {"Amount": -1}}
        ,{"$limit": 3}
    ]
   
    cursor=collection.aggregate(pipeline)
    mongo.close()
    return cursor
   
for doc in get_cart():
    print("")
    print("Name : "+doc["Name"]+"       Amount : "+str(doc["Amount"])+"     Quantity :"+str(doc["Quantity"]))
    print("--------------------------------------------------------------------")
    for product in doc["Product"]:
        print(product[0]+" ( "+str(product[1])+" * "+str(product[2])+"  =  "+str(product[3])+" )")
   
    print("--------------------------------------------------------------------")
    print("")

