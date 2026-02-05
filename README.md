# Project Name: FMI

RESTful API built with ASP.NET Core Web Api 8.0 This project was started with educational purpose and 
is supposed to serve as a library.

FMI stands for the Faculty of Mathematics and Informatics of the Sofia University "St. Kliment Ohridski".


## Features

- Register and Login endpoints
- JWT authentication
- Google OAuth 2.0
- Open Api


## Tech Stack

- Framework: ASP.NET Core Web Api 8.0
- Database: MongoDB


## Installation & Setup

1. Clone the repository:

``` bash
git clone https://github.com/your-username/your-repo-name.git
cd your-repo-name
```

2. Set the db link:

 In "appsettings.json" put your MongoDb connection string.

``` json
"ConnectionStrings": {
    "MongoDb": "mongodb://your-db-string"
  }
```

3. Run the application


### How to get Google ID Token using Postman 

1. ![Postman Config Part 1](fmi\Assets\Postman_Google_OAuth_1.PNG)

Auth URL:  https://accounts.google.com/o/oauth2/v2/auth
Scope: https://www.googleapis.com/auth/userinfo.email

2. ![Postman Config Part 2](fmi\Assets\Postman_Google_OAuth_2.PNG)

3. You will receive Access Token and ID Token.
Copy the ID Token and provide it for the ../fmi/google-signin endpoint.

Access Token: Used to call Google APIs.
ID Token: Used to prove identity to your API.
