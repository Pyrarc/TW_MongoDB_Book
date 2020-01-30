//12.4  查詢文檔資料
package MongoDB;
import static com.mongodb.client.model.Accumulators.avg;
import static com.mongodb.client.model.Accumulators.max;
import static com.mongodb.client.model.Accumulators.min;
import static com.mongodb.client.model.Accumulators.sum;
import static com.mongodb.client.model.Aggregates.group;
import static com.mongodb.client.model.Sorts.descending;
import static com.mongodb.client.model.Projections.fields;
import static com.mongodb.client.model.Projections.include;
import static com.mongodb.client.model.Projections.excludeId;
import java.util.Arrays;
import org.bson.Document;
import org.bson.conversions.Bson;
import com.mongodb.Block;
import com.mongodb.MongoClient;
import com.mongodb.client.AggregateIterable;
import com.mongodb.client.FindIterable;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.model.Filters;
import com.mongodb.client.model.Indexes;
public class MongoDB_Java_12_4 {
    public static void main(String args[]) {         
         
         //取得mongo用戶端
         MongoClient mongoClient = MongoDBClient.getMongoClient();
         //取得Members集合
         MongoCollection<Document>  mongoCollection =
         mongoClient.getDatabase(MongoDBClient.getDbname()).getCollection("Members");
         
     
//---12.4.1 限制查詢結果集的大小----------------
    
    //-1. 查詢全部文檔
         //迴圈輸出
         for (Document cur : mongoCollection.find()) {
             System.out.println(cur.toJson());
         }
         
    //-2. 限制查詢結果集的大小
         for (Document cur : mongoCollection.find().limit(2)) {
              System.out.println(cur.toJson());
         }
//--------------------------------------------         
//---12.4.2  限制查詢返回的欄位------------------         
         
    //-1. 返回特定的欄位     
         FindIterable<Document>  findIterable = mongoCollection
                   .find()
                   //指定只顯示Name、Tel欄位
                   .projection(fields(include("Name", "Tel")));
         for (Document cur : findIterable) {
                  System.out.println(cur.toJson());
         }
         
    //-------------------------         
    //-2. 返回除_id以外的特定欄位    
         FindIterable<Document>  findIterable2 = mongoCollection
                       .find()                 
                       //指定只顯示Name、Tel欄位，並排除excludeId
                       .projection(fields(include("Name", "Tel"),excludeId()));
         for (Document cur : findIterable2) {
                   System.out.println(cur.toJson());
         }
         
//--------------------------------------------
//---12.4.3  按條件進行查詢-------------------
         
    //-1. 單條件查詢
         FindIterable<Document>  findIterable3 = 
                 mongoCollection.find(Filters.eq("CustomerSysNo",11735490));
         for (Document cur : findIterable3) {
               System.out.println(cur.toJson());
         }
         
    //-2. 多條件查詢
        //定義文檔塊
         Block<Document> printBlock = new Block<Document>() {
            @Override
            public void apply(final Document document) {
                System.out.println(document.toJson());
            }
         };
         mongoCollection
                .find(Filters.and(
                        //大於11700000
                        Filters.gt("CustomerSysNo",11700000),
                        //小於11760000
                        Filters.lt("CustomerSysNo",11760000)
                ))
                 //反覆運算列印文檔塊
                .forEach(printBlock);
         
//--------------------------------------------
//---12.4.4  對查詢結果分頁-------------------        
    
    //-2.編寫分頁函數findByPage()-需在main()函數外編寫，至代碼末端查看
         
    //-3. 測試分頁函數
         Block<Document> printBlock1 = new Block<Document>() {
             @Override
             public void apply(final Document document) {
                 System.out.println(document.toJson());
             }
         };         
         //調用分頁函數
         findByPage(mongoCollection,
                 //查詢過濾條件
                 Filters.and(
                         Filters.gt("CustomerSysNo",1170123),
                         Filters.lt("CustomerSysNo",11760000)
                 ),
                 descending("CustomerSysNo"),
                 1,
                 2).forEach(printBlock1);
         
         
//--------------------------------------------         
//---12.4.5  用聚合方法查詢文檔-------------------    
             
         //集合使用Carts
         MongoCollection<Document>  mongoCollection1 =
                 mongoClient.getDatabase(MongoDBClient.getDbname()).getCollection("Carts");
         
          AggregateIterable<Document> DocAgg = mongoCollection1.aggregate(Arrays.asList(
                    //分組匯總Quantity
                    group("Total",sum("totalQuantity", "$Quantity"),
                            //求平均值
                            avg("avgQuantity", "$Quantity"),
                            //求最大值
                            max("maxQuantity", "$Quantity"),
                            //求最小值
                            min("minQuantity", "$Quantity"))
          ));
          for (Document cur : DocAgg)
          {
             System.out.println(cur.toJson());
          }
          
//-------------------------------------------
//---12.4.6  應用索引查詢-----------------------    
          
          //集合使用Members
          MongoCollection<Document>  mongoCollection2 =
                     mongoClient.getDatabase(MongoDBClient.getDbname()).getCollection("Members");
                  
          //（1）對"Name"欄位設置昇冪的索引
          mongoCollection2.createIndex(Indexes.ascending("Name"));
          
          //（2）對"Name"欄位設置降冪的索引
          mongoCollection2.createIndex(Indexes.descending("Name"));
          
          //（3）對"Name""CustomerSysNo"這兩欄位設置複合降冪的索引
          mongoCollection2.createIndex(Indexes.descending("Name","CustomerSysNo"));
         
          //（4）對"Name"欄位設置降冪索引，對"CustomerSysNo"欄位設置昇冪索引
          mongoCollection2.createIndex(Indexes.compoundIndex(Indexes.descending("Name"), Indexes.ascending("CustomerSysNo")));
          
          //（5）建立Hash索引
          mongoCollection2.createIndex(Indexes.hashed("_id"));
          
          
          //集合使用Product
          MongoCollection<Document>  mongoCollection3 =
                     mongoClient.getDatabase(MongoDBClient.getDbname()).getCollection("Product");
          
          //（6）創建文字索引
          mongoCollection3.createIndex(Indexes.text("ProductName"));
          //查詢"ProductName"欄位中所有包含"Gold"關鍵字的文檔
          Bson textSearch = Filters.text("Gold");
          for (Document cur : mongoCollection3.find(textSearch)) {
               System.out.println(cur.toJson());
          }
//-------------------------------------------
      
          
    }
    
    //12.4.4-2.編寫分頁函數
         public static FindIterable<Document> findByPage(
                    MongoCollection<Document>  mongoCollection,
                    Bson filter,Bson orderBy,int pageNo, int pageSize) {
                          return mongoCollection
                            .find(filter)
                            //排序
                           .sort(orderBy)
                            //跳轉至第pageNo頁的取pageSize個文檔
                           .skip((pageNo - 1) * pageSize).limit(pageSize);
             }
}
   
