//12.3  應用與操作
package MongoDB;
import java.util.ArrayList;
import java.util.List;
import org.bson.Document;
import com.mongodb.MongoClient;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.model.Filters;
public class MongoDB_Java_12_3 {
     public static void main(String args[]) {         
         
         //取得mongo用戶端
 	MongoClient mongoClient = MongoDBClient.getMongoClient();
 	//取得Members集合
 	MongoCollection<Document>  mongoCollection =mongoClient.getDatabase(MongoDBClient.getDbname()).getCollection("Members");

         
         
//---12.3.1  新增文檔-------------------------
    //-1. 插入單個文檔
         
         //創建一個文檔
 	Document doc = new Document("Name", "Deng")
        	.append("Gender","Male")
       		.append("Tel","18310001000")
        	.append("CustomerSysNo", 11753090)
        	.append("Location", 
                	new Document("City", "Keelung").append("Dist", "Xinyi")
                );
 	//插入文檔
 	mongoCollection.insertOne(doc);

    //-----------------------     
    //-2.插入多個文檔
         
         //定義文檔列表
 	List<Document> docs = new ArrayList<Document>();
 	//創建文檔doc1
 	Document doc1 = new Document("Name", "Yun")
        	.append("Gender","Male")
        	.append("Tel","18319991999")
        	.append("CustomerSysNo", 11735490)
        	.append("Location",
                	new Document("City", "Tainan").append("Dist", "Annan")
        	);
 	//創建文檔doc2
 	Document doc2 = new Document("Name", "Qiang")
        	.append("Gender","Male")
        	.append("Tel","18318881888")
        	.append("CustomerSysNo", 11704571)
        	.append("Location",
                	new Document("City", " Tainan").append("Dist", "Anping")
        	);
 	//將文檔doc1添加至列表中
 	docs.add(doc1);

 	//將文檔doc2添加至列表中
 	docs.add(doc2);

 	//將清單中的文檔全部寫入MongoDB
 	mongoCollection.insertMany(docs);

    //-----------------------
 
//--------------------------------------------         
//---12.3.2  刪除文檔-------------------------
         
         Document docDel = new Document("Tel", "18319991999");
         //-1. 刪除符合條件的第1個文檔
         mongoCollection.deleteOne(docDel);
         
         
         //-2. 刪除符合條件的全部文檔     
         mongoCollection.deleteMany(docDel);
         
         
         //-3. 刪除全部資料和集合
         mongoCollection.drop();
         
//--------------------------------------------
//---12.3.3  修改文檔-------------------------
         
         Document docUpd =new Document("$set",new Document("Tel","18323332333"));
         
         //-1. 修改第1個符合條件的文檔
         mongoCollection.updateOne(Filters.eq("CustomerSysNo", 11704571), docUpd);
         
         //-2. 批量修改文檔
         mongoCollection.updateMany(Filters.gt("CustomerSysNo", 11700000), docUpd);
//--------------------------------------------
         
         
         
         //關閉連線
         MongoDBClient.close();     
         
        } 
}
