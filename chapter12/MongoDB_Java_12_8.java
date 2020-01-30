//12.8  操作MongoDB GridFS
package MongoDB;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.file.Files;
import org.bson.Document;
import org.bson.types.ObjectId;
import com.mongodb.Block;
import com.mongodb.MongoClient;
import com.mongodb.client.MongoDatabase;
import com.mongodb.client.gridfs.GridFSBucket;
import com.mongodb.client.gridfs.GridFSBuckets;
import com.mongodb.client.gridfs.GridFSUploadStream;
import com.mongodb.client.gridfs.model.GridFSDownloadOptions;
import com.mongodb.client.gridfs.model.GridFSFile;
import com.mongodb.client.gridfs.model.GridFSUploadOptions;
import com.mongodb.client.model.Filters;
public class MongoDB_Java_12_8 {
    public static void main(String args[]) {
        
         MongoClient mongoClient = MongoDBClient.getMongoClient();
         MongoDatabase mongoDatabase = mongoClient.getDatabase
                (MongoDBClient.getDbname());
        
     //-1. 上傳文件
         
         //（1）uploadFromStream()方法。
         GridFSBucket gridFSBucket = GridFSBuckets.create(mongoDatabase, "fsmp4");
         try {
              //取得本地的MP4檔
              String filePath=System.getProperty("user.dir")+"/upload/mongoDB.mp4";
              //讀入文件流
              InputStream inputStream = new FileInputStream(new File(filePath));
              //設置塊大小及檔案類型
              GridFSUploadOptions options = new GridFSUploadOptions()
                    .chunkSizeBytes(358400)
                    .metadata(new Document("type", "mp4"));
              //上傳，並取得檔fileId
              ObjectId fileId = gridFSBucket
                   .uploadFromStream("mongodb-mp4", inputStream, options);
              
              System.out.println("The fileId of the uploaded file is: " 
                                                        +  fileId.toHexString());
         } catch (FileNotFoundException e){
              e.printStackTrace();
         }
         
         //（2）openUploadStream()方法。
         GridFSBucket gridFSBucket1 = GridFSBuckets.create(mongoDatabase, "fsmp4");
         try {
              String filePath=System.getProperty("user.dir")+"/upload/mongoDB.mp4";
              GridFSUploadOptions options = new GridFSUploadOptions()
                   .chunkSizeBytes(358400)
                   .metadata(new Document("type", "mp4"));
              //打開上傳流
              GridFSUploadStream uploadStream = gridFSBucket1
                   .openUploadStream("mongodb_02-mp4", options);
              //讀取檔二進位
              byte[] data = Files.readAllBytes(new File(filePath).toPath());
              //將上傳文件寫入MongoDB
              uploadStream.write(data);  
              uploadStream.close();
              System.out.println("The fileId of the uploaded file is: "
                    + uploadStream.getObjectId().toHexString());
         } catch(IOException e){
              e.printStackTrace();
        }
         
    //-2. 下載檔案
         
         //（1）以ObjectId為條件下載檔案。
         try {
              String filePath=System.getProperty("user.dir")+"/download/mongoDB.mp4";
              //取得輸出流
              FileOutputStream streamToDownloadTo = new FileOutputStream(filePath);
              ObjectId fileId =new ObjectId("5ade84f8247b041748b981ba");  //OjbectId鬚根據下載檔案替換
              //根據OjbectId下載
              gridFSBucket.downloadToStream(fileId, streamToDownloadTo);
              streamToDownloadTo.close();
              System.out.println(streamToDownloadTo.toString());
            } catch (IOException e) {
         }
         
         //（2）以檔案名為條件下載。
         try {
             String filePath=System.getProperty("user.dir")+"/download/mongoDB.mp4";
             FileOutputStream streamToDownloadTo = new FileOutputStream(filePath);
             //取得下載版本
             GridFSDownloadOptions downloadOptions = new GridFSDownloadOptions().revision(0);
             //下載檔案名為"mongodb-mp4"的文件
             gridFSBucket.downloadToStream("mongodb-mp4", 
                    streamToDownloadTo, downloadOptions);
             streamToDownloadTo.close();
         } catch (IOException e) {
         } 
         
    //查找GridFS的文檔
         gridFSBucket.find(Filters.eq("metadata.type", "mp4"))
            .forEach(
            new Block<GridFSFile>() {
                public void apply(final GridFSFile gridFSFile) {
                    System.out.println(gridFSFile.getFilename());
                }
     });
    //刪除GridFS檔
         ObjectId fileId =new ObjectId("5ade8910247b0418283a472f"); //OjbectId鬚根據刪除檔替換
         gridFSBucket.delete(fileId);
    }
    
}

