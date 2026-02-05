using MongoDB.Driver;
using fmi.Models;
using fmi.Config;
using Google.Apis.Auth;



namespace fmi.Services
{
    public class UserService
    {
        private IMongoCollection<User> usersCollection;
        private JwtAuthService jwtAuthService;
        private readonly IConfiguration config;

        public UserService(MongoDbService mongoDbService, JwtAuthService authService, IConfiguration config)
        {
            usersCollection = mongoDbService.GetCollection<User>(Constants.Database.FMI_DB, Constants.Database.FMI_DB_COLLECTION_USERS);
            jwtAuthService = authService;
            this.config = config;
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

            if (savedUser == null || PasswordHasher.VerifyPassword(userDto.Password, savedUser.Password))
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Invalid username or password.",
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }

            var accessToken = jwtAuthService.GenerateAccessToken(savedUser);

            return new ServiceResponse<string>
            {
                Data = accessToken,                
                Message = "Successfully Logged in."                
            };
        }


        public async Task<string> AuthenticateGoogleUser(string googleToken)
        {
            var payload = await VerifyToken(googleToken);

            // Check if the user exists
            var user = await usersCollection.Find(u => u.Email == payload.Email).FirstOrDefaultAsync();

            // var accessToken = string.Empty; 

            if(null == user)
            {
                user = new User();
                user.Email = payload.Email;
                user.Username = payload.Email;
                user.GoogleId = payload.Subject;
                user.Password = null;

                await usersCollection.InsertOneAsync(user);

                return jwtAuthService.GenerateAccessToken(user);
            }

            // Link Google ID to an existing email-registered account

            else if (string.IsNullOrEmpty(user.GoogleId))
            {
                var filter = Builders<User>.Filter.Eq(u => u.Email, payload.Email);
                var update = Builders<User>.Update.Set(u => u.GoogleId, payload.Subject);
                
                await usersCollection.UpdateOneAsync(filter, update);
            }

            return jwtAuthService.GenerateAccessToken(user);
        }


        public async Task<GoogleJsonWebSignature.Payload> VerifyToken(string googleToken)
        {
            try
            {
                // Validate the token with Google
                var validationSettings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { config["Authentication:Google:ClientId"] }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(googleToken, validationSettings);

                return payload;
            }
            catch (InvalidJwtException)
            {
                throw new Exception("Invalid Google token.");
            }
        }

    }
}
