# AuthControl API

AuthControl is a simple and secure API built for user registration and authentication using JWT (JSON Web Tokens) and role-based access control.

## Features

- User registration with validation.
- JWT-based authentication.
- Role-based authorization (Admin role).
- Swagger documentation for easy API exploration.

## Technologies Used

- .NET 8.0 
- ASP.NET Core
- Dapper Micro ORM
- FluentValidation for input validation
- Swagger for API documentation

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or later)
- SQL Server

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/dasunmahesha/AuthControl.git
   cd AuthControl

2. Restore the required packages:

   ```bash
    dotnet restore

3. Configure your database connection string in appsettings.json.

4. Run the application:
   ```bash
    dotnet run

5. Setup databse

    Create the database using the provided SQL script

6. Default User
    ```json
    {
      "username": "admin",
      "password": "admin"
    }   

## API Documentation

### The API documentation is available at /swagger when the application is running. You can test the endpoints directly in your browser.

## Endpoints

### post /api/Auth/login: login.
#### Sample Request Body for lOGIN
    ```json
    {
      "username": "string",
      "password": "string"
    }

#### Sample responce

    
    {
      "token": "eyJhbcdW1......du97laWwRT58ZI"
    }

### POST /api/auth/register: Register a new user (Admin role required).


#### Sample Request Body for Registration
    
    {
    "username": "string",
    "password": "string",
    "passwordconfirmation": "string",
    "email": "string",
    "role": 0 // 0 for user, 1 for admin, etc.
    }
#### Sample responce
   
    {
      "message": "User registered successfully"
    }

### get /api/Auth/error: To check Error handling middleware
#### Sample responce
   
    {
      "StatusCode": 500,
      "Message": "Internal Server Error."
    }


## Author

- Dasun Mahesha
- [LinkedIn Profile](https://www.linkedin.com/in/dasun-mahesha/)

