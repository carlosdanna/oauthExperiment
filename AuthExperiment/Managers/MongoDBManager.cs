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

        #region Constructor
        public MongoDBManager()
        {
            client = new MongoClient("mongodb://user:password@ds023088.mlab.com:23088/signuptest");
            clientDatabase = client.GetDatabase("signuptest");
        }
        #endregion

        //public IMongoCollection<BsonDocument> GetCollection(string CollectionName)
        //{
        //    return clientDatabase.GetCollection<BsonDocument>(CollectionName);
        //}

        #region Public Methods
        /// <summary>
        /// Get Users from the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserModel>> GetUsers()
        {
            var collection = clientDatabase.GetCollection<BsonDocument>("users");
            
            BsonDocument filter = new BsonDocument();
            var search = await collection.FindAsync<BsonDocument>(filter);
            
            List<UserModel> users = new List<UserModel>();

            foreach (BsonDocument item in search.ToList<BsonDocument>())
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

        public async Task<UserModel> GetUser(LoginModel user)
        { 
            
            var collection = clientDatabase.GetCollection<BsonDocument>("users");
            UserModel userToLogin = new UserModel();
            //Find user by e-mail and check if the password matches
            
            BsonDocument filter = new BsonDocument();
            filter.Set("email", user.email);
            filter.Set("password", user.password);
            
            var search = await collection.FindAsync<BsonDocument>(filter);
            var result = search.ToList<BsonDocument>().First<BsonDocument>();

            userToLogin._id = result.GetValue("_id").ToString();
            userToLogin.email = result.GetValue("email").ToString();
            userToLogin.firstname = result.GetValue("firstname").ToString();
            userToLogin.lastname = result.GetValue("lastname").ToString();
            userToLogin.password = result.GetValue("password").ToString();

            
            return userToLogin;
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