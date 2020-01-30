#!/usr/bin/python
#This Python file uses the following encoding:utf-8
  
#14.8  操作MongoDB GridFS
#2. 實際操作
    
import pymongo
from bson.objectid import ObjectId
from pprint import pprint
import gridfs
import os
    
def test_gridfs():
    mongodb_server_uri="mongodb://<mongodb_user>:<mongodb_pwd>@127.0.0.1:27017"
    mongo = pymongo.MongoClient(mongodb_server_uri)
    db = mongo["E-commerce"]
    fs = gridfs.GridFS(db,"images")
  
    #打開一個視頻檔
    video = open(r"test.mp4","r") #目前的目錄
     
    #將檔存入資料庫後，資料庫會自動生成一個id
    gf_id = fs.put(video, filename="test.mp4", format="mp4")
    print(gf_id)
   
    #讀取images.chunks集合，該集合為存儲GridFS檔的預設集合
    cursor=db["images.chunks"].find()
  
    #以id為條件來查詢檔
    gf = fs.get(gf_id)
   
    #讀取GridFS文件
    im = gf.read()
    doc = {}
    doc ["chunk_size"] = gf.chunk_size
    doc ["metadata"] = gf.metadata
    doc ["length"] = gf.length
    doc ["upload_date"] = gf.upload_date
    doc ["name"] = gf.name
    doc ["content_type"] = gf.content_type
       
    pprint(doc)
     
    #將GridFS檔存儲到本地磁片
    output = open("test_bak.mp4", 'wb')
    output.write(im)
    output.close()
     
    mongo.close()
    return cursor
      
test_gridfs()

