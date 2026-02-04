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
