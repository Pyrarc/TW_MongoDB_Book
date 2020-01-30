（2）「mongodb.properties」檔案
mongodb.url=mongodb://user1:pwd1@host1:27017,host2:27017,host3:27017/?replicaSet=myRepSet


（3）「MongoDBClient」類別
public class MongoDBClient {
    private MongoDBClient() {
    }
    //定義MongoClient，初始為空值
    private static MongoClient mongoClient =null;
    //定義資料庫名，初始為空值
    private static String dbname = null;
    static {
        if ( mongoClient == null) {
            synchronized (MongoClient.class) {
                try {
                    ResourceBundle resourceBundle =                    
                              ResourceBundle.getBundle("mongodb");
                    //取得MongoDB的連線字串
                    String mongodbURL = resourceBundle.getString("mongodb.url");


                    if (mongodbURL == null || mongodbURL.trim().equals("")) {
                        //預設為本機資料庫
                        mongoClient = new MongoClient();
                    } else {
                        //取得MongoClientURL物件
                        MongoClientURI clientURI = new MongoClientURI(mongodbURL);
                        //指派mongoClient 
                        mongoClient = new MongoClient(clientURI);
                        //指派dbname
                        dbname=clientURI.getDatabase();
                    }
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
    }
    

public static  MongoClient getMongoClient() {
       //返回mongoClient
       return mongoClient;
    }
    public static String getDbname() {
       //返回dbname
        return dbname;
    }
    public static void close() {
        //關閉連線
        mongoClient.close();
    }
 }



（4）「PrintDB」的類別
public class PrintDB {
   public static void main(String[] args) {
     //列印出伺服器的所有資料庫
     for (String databaseName :
                     MongoDBClient.getMongoClient().listDatabaseNames())       
     {
         System.out.println("Database: " + databaseName);
     }
     //關閉連線
     MongoDBClient.close();
  }
}
