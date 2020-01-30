//12.5  使用規則運算式
package MongoDB;
import java.util.regex.Pattern;
import org.bson.Document;
import org.bson.conversions.Bson;
import com.mongodb.MongoClient;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.model.Filters;
public class MongoDB_Java_12_5 {
    public static void main(String args[]) {
        
         //取得mongo用戶端
         MongoClient mongoClient = MongoDBClient.getMongoClient();
         //取得Members集合
         MongoCollection<Document>  mongoCollection =
         mongoClient.getDatabase(MongoDBClient.getDbname()).getCollection("Members");
         
         //定義正則式，不區分大小寫
         Pattern regex = Pattern.compile("yun", Pattern.CASE_INSENSITIVE);
         Bson filter = Filters.eq("Name", regex);
         for (Document cur : mongoCollection.find(filter)) {
              System.out.println(cur.toJson());
         }
        
    }
}
 
