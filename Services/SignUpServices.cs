using MainProject.SignUp.Interface;
using MainProject.SignUp.Model;
using MainProject.DataBase.Interface;
using MongoDB.Driver;

namespace MainProject.SignUp.Services
{
    public class SignUpServices : ISignUpServices
    {
        private readonly IDatabaseCollections _databaseCollections;

        public SignUpServices(IDatabaseCollections databaseCollections)
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
