using MovieApp.Models;
using MongoDB.Driver;
using MovieApp.Database;
using MovieApp.Services;

namespace MainProject.SignUp.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly IDatabaseCollections _databaseCollections;

        public SignUpService(IDatabaseCollections databaseCollections)
        {
            _databaseCollections = databaseCollections;
        }

        public bool SignUpMethod(SignUpModel request)
        {
            var UserInDB = _databaseCollections.UserDetails().Find(x => x.EmailAddress == request.EmailAddress).SingleOrDefault();
            if (UserInDB != null)
            {
                return false;
            }
            else
            {
                _databaseCollections.UserDetails().InsertOne(request);

                var filter = Builders<SignUpModel>.Filter.Empty; 
                var update = Builders<SignUpModel>.Update.Unset("ConfirmPassword");
                
                _databaseCollections.UserDetails().UpdateMany(filter, update);
                return true;
            }
        }
    }
}
