//12.7  創建文檔關聯查詢
package MongoDB;
import static com.mongodb.client.model.Aggregates.lookup;
import static com.mongodb.client.model.Aggregates.match;
import static com.mongodb.client.model.Aggregates.project;
import static com.mongodb.client.model.Projections.excludeId;
import static com.mongodb.client.model.Projections.fields;
import static com.mongodb.client.model.Projections.include;
import java.util.Arrays;
import org.bson.Document;
import com.mongodb.MongoClient;
import com.mongodb.client.AggregateIterable;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.model.Filters;
public class MongoDB_Java_12_7 {
    public static void main(String args[]) {
        
         //取得mongo用戶端
         MongoClient mongoClient = MongoDBClient.getMongoClient();
         //取得Members集合
         MongoCollection<Document>  mongoCollection =
         mongoClient.getDatabase(MongoDBClient.getDbname()).getCollection("Members");
         
         AggregateIterable<Document> DocLK = mongoCollection.aggregate(
                 Arrays.asList(
                        project(fields(include("Name","Tel","CustomerSysNo",
                                "Quantity","product"), excludeId())),
                        //Name為Yun Deng
                        match(Filters.eq("Name","Yun Deng")),
                        /* 
                              連接購物車(Carts)，連接欄位為CustomerSysNo
                              lookup屬性值格式如：lookup (from,LocalField,foreignField,as)
                        */                      
                        lookup("Carts","CustomerSysNo","CustomerSysNo","MyCarts")
                )
         );
         for (Document cur : DocLK)
         {
              System.out.println(cur.toJson());
         }
    }
}

