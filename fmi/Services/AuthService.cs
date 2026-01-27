using MongoDB.Driver;
using fmi.Models;
using fmi.Config;



namespace fmi.Services
{
    public class AuthService
    {
        IMongoCollection<User> usersCollection;

        public AuthService(MongoDbService mongoDbService)
        {
            usersCollection = mongoDbService.GetCollection<User> (Constants.Database.FMI_DB, Constants.Database.FMI_DB_COLLECTION_USERS);
        }

        public async Task<Models.ServiceResponse<string>> Register(UserDto userDto)
        {
            var existingUser = await usersCollection.Find(u => u.Username == userDto.Username).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return new ServiceResponse<string> {

                    Success = false,
                    Message = "User Already Exists.",
                    StatusCode = StatusCodes.Status409Conflict
                };
            }

            var newUser = new User();
            newUser.Username = userDto.Username;
            newUser.Password = PasswordHasher.HashPassword(userDto.Password);

            await usersCollection.InsertOneAsync(newUser);

            return new ServiceResponse<string>
            {
                Data = newUser.Username,
                Success = true,
                Message = "User Registered Successfully.",
                StatusCode = StatusCodes.Status201Created
            };

        }

        /// public string and should return the access token
        public async Task<Models.ServiceResponse<string>> Login(UserDto userDto)
        {
            var savedUser = await usersCollection.Find(u => u.Username == userDto.Username).FirstOrDefaultAsync();

            if (savedUser == null || !PasswordHasher.VerifyPassword(userDto.Password, savedUser.Password))
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Invalid username or password.",
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }

            return new ServiceResponse<string>
            {
                Data = savedUser.Username,                
                Message = "Successfully Logged in."                
            };
        }

    }
}
