//12.6  批量處理資料
package MongoDB;
import java.util.Arrays;
import org.bson.Document;
import com.mongodb.MongoClient;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.model.DeleteOneModel;
import com.mongodb.client.model.InsertOneModel;
import com.mongodb.client.model.ReplaceOneModel;
import com.mongodb.client.model.UpdateOneModel;
import com.mongodb.client.model.BulkWriteOptions;
public class MonogDB_Java_12_6 {
    public static void main(String args[]) {
        
         //取得mongo用戶端
         MongoClient mongoClient = MongoDBClient.getMongoClient();
         //取得Members集合
         MongoCollection<Document>  mongoCollection =
         mongoClient.getDatabase(MongoDBClient.getDbname()).getCollection("Members");
         
         
    //-1. BulkWrite()
         //批量操作
         mongoCollection.bulkWrite(
                 Arrays.asList(
                     //新增
                     new InsertOneModel<>(new Document("Name", "Deng")),
                     new InsertOneModel<>(new Document("Name", "Yun")),
                     new InsertOneModel<>(new Document("Name", "Qiang")),
                     new InsertOneModel<>(new Document("Name", "WuGe")),
                     //修改
                     new UpdateOneModel<>(new Document("Name", "Deng"),
                         new Document("$set", new Document("Gender", "Male"))),
                     //刪除
                     new DeleteOneModel<>(new Document("Name", "WuGe")),
                     //替換
                     new ReplaceOneModel<>(new Document("Name", "Qiang"),         
                     new Document("Name", "Qiang").append("Gender", "Male")))
        );
    //-2. ordered(false)
         mongoCollection.bulkWrite(
                 Arrays.asList(new InsertOneModel<>(new Document("Name", "Deng")),
                     new InsertOneModel<>(new Document("Name", "Yun")),
                     new InsertOneModel<>(new Document("Name", "Qiang")),
                     new InsertOneModel<>(new Document("Name", "WuGe")),
                     new UpdateOneModel<>(new Document("Name", "Deng"),
                     new Document("$set", new Document("Gender", "Male"))),
                     new DeleteOneModel<>(new Document("Name", "WuGe")),
                     new ReplaceOneModel<>(new Document("Name", "Qiang"),
                     new Document("Name", "Qiang").append("Gender", "Male"))),
                     //忽略操作順序
                     new BulkWriteOptions().ordered(false)
             );
         
    }
}

