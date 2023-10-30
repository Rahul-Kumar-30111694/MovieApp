using MovieApp.Database;
using MovieApp.Models;
using MongoDB.Driver;

namespace MovieApp.Services
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly IDatabaseCollections _databaseCollections;

        public ForgotPasswordService(IDatabaseCollections databaseCollections)
        {
            _databaseCollections = databaseCollections;
        }

        public bool ForgotPasswordMethod(ForgotPasswordModel request)
        {
            dynamic UserInDB = _databaseCollections.UserDetails().Find(x => x.EmailAddress == request.EmailAddress).SingleOrDefault();
            if(UserInDB == null)
            {
                return false;
            }
            else
            {
                var filter = Builders<SignUpModel>.Filter.Eq(x => x.EmailAddress, request.EmailAddress);
                var update = Builders<SignUpModel>.Update.Set(x => x.Password, request.Password);

                _databaseCollections.UserDetails().UpdateOne(filter, update);
                return true;
            }
        }
    }
}