using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
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

            foreach (BsonDocument item in search) {
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
        #endregion
    }
}