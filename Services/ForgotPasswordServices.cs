using MovieApp.Database;
using MainProject.ForgotPassword.Interface;
using MovieApp.Model;
using MongoDB.Driver;

namespace MainProject.ForgotPassword.Services
{
    public class ForgotPasswordServices : IForgotPasswordServices
    {
        private readonly IDatabaseCollections _databaseCollections;

        public ForgotPasswordServices(IDatabaseCollections databaseCollections)
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