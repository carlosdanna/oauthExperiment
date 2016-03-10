using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using AuthExperiment.Models;
using System.Threading.Tasks;


namespace AuthExperiment.Managers
{
    public class MongoDBManager
    {
        #region Private methods
        private MongoClient client;
        private IMongoDatabase clientDatabase;
        #endregion

        #region Public Methods
        public MongoDBManager()
        {
            client = new MongoClient("mongodb://user:password@ds023088.mlab.com:23088/signuptest");
            clientDatabase = client.GetDatabase("signuptest");
        }

        public IMongoCollection<BsonDocument> GetCollection(string CollectionName)
        {
            return clientDatabase.GetCollection<BsonDocument>(CollectionName);
        }

        public async Task<List<UserModel>> GetUsers()
        {
            var collection = clientDatabase.GetCollection<BsonDocument>("users");
            List<BsonDocument> search = new List<BsonDocument>();
            BsonDocument filter = new BsonDocument();
            search = collection.Find<BsonDocument>(filter).ToListAsync().Result;

            List<UserModel> users = new List<UserModel>();

            foreach (BsonDocument item in search)
            {
                UserModel user = new UserModel();
                user._id = item.GetValue("_id").ToString();
                user.email = item.GetValue("email").ToString();
                user.firstname = item.GetValue("firstname").ToString();
                user.lastname = item.GetValue("lastname").ToString();
                user.password = item.GetValue("password").ToString();
                users.Add(user);
            }
            return users;

        }

        public async Task CreateNewUser(UserModel user)
        {
            var type = user.GetType();
            var properties = type.GetProperties();
            BsonDocument bsonUser = new BsonDocument();


            foreach (var property in properties)
            {
                if (property.Name.ToString() == "_id") {
                    bsonUser.Set(property.Name.ToString(), new ObjectId());
                    continue;
                }
                if (property.GetValue(user) == null)
                {
                    bsonUser.Set(property.Name.ToString(), "");
                }
                else
                {
                    bsonUser.Set(property.Name.ToString(), property.GetValue(user).ToString());
                }
            }
            var collection = clientDatabase.GetCollection<BsonDocument>("users");
            
            await collection.InsertOneAsync(bsonUser);

        }
        #endregion
    }
}