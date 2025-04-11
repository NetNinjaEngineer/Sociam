# Sociam API

Sociam API is a modern social media platform backend that enables real-time messaging, group interactions, notifications, and seamless social connections. Build engaging social experiences with our robust, scalable API.

## ✨ Features

- **Real-time messaging** with SignalR
- **Group management** for communities
- **Social interactions** (posts, stories, likes, comments)
- **Comprehensive notification system**
- **Secure authentication** with JWT, 2FA, and social logins
- **Role-based permissions**

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core 9
- **Database**: PostgreSQL with Entity Framework Core
- **Real-time**: SignalR
- **Authentication**: JWT, Identity Framework
- **Other tools**: AutoMapper, FluentValidation, QRCoder

## 🏗️ Architecture

Sociam follows clean architecture principles:
- API Layer - Request handling
- Application Layer - CQRS pattern
- Domain Layer - Core entities
- Infrastructure Layer - External integrations
- Persistence Layer - Data access with repository pattern
- Services Layer - Business logic

## 🚀 Getting Started

### Prerequisites
- .NET 9 SDK
- PostgreSQL
- Git

### Installation

1. Clone the repository
   ```sh
   git clone https://github.com/NetNinjaEngineer/Sociam.git
   ```

2. Configure `appsettings.json` with your credentials:
   ```json
   {
      "ConnectionStrings": {
         "DefaultConnection": "",
         "PostgresConnection": ""
      },
      "JwtSettings": {
         "Key": "yoursecretkey",
         "Audience": "https://localhost:7042",
         "Issuer": "https://localhost:7042",
         "ExpirationInDays": 1
      },
      "Authentication": {
         "GoogleOptions": {
            "ClientId": "",
            "ClientSecret": ""
         },
         "FacebookOptions": {
            "AppId": "",
            "AppSecret": ""
         }
      },
      "SmtpSettings": {
         "Gmail": {
            "SenderName": "Sociam",
            "SenderEmail": "Your email",
            "Host": "smtp.gmail.com",
            "Port": 587,
            "Password": "yourpasswordfromgoogleapppasswords"
         }
      },
      "ApiKey": "",
      "AuthCodeExpirationInMinutes": 3,
      "BaseApiUrl": "https://localhost:7042",
      "FullbackUrl": "http://localhost:5237",
      "FormOptionsSize": "1073741824",
      "StoryExpirationCheckIntervalDays": 1,
      "CacheExpirationInMinutes": 1,
      "IpInfoOptions": {
         "BaseUrl": "https://ipinfo.io",
         "Token": "yourtoken"
      },
      "IpGeoLocationOptions": {
         "BaseUrl": "https://api.ipgeolocation.io",
         "ApiKey": "yourapikey"
      }
   }
   ```

3. Create the initial database migration:
   ```sh
   # From Package Manager Console with Sociam.Infrastructure as default project
   add-migration Initial -o Persistence/Migrations
   ```

4. Apply migrations to your database:
   ```sh
   Update-Database
   ```

5. Run the application:
   ```sh
   dotnet run
   ```

## 📚 API Documentation

Explore the full API using Swagger: [https://sociam.runasp.net/swagger/index.html](https://sociam.runasp.net/swagger/index.html)

### Key Endpoints

#### Authentication
- Sign up, login, social auth
- Password recovery
- 2FA/MFA management

#### User Management
- Profile updates
- Avatar/cover photo changes
- Email/password management

#### Messaging
- Private & group messaging
- Real-time updates
- Message history & search

#### Groups
- Create & manage groups
- Membership control
- Group messaging

#### Posts & Stories
- Create, edit, delete posts
- Like & comment
- Time-limited stories

#### Notifications
- Delivery system
- Read/unread management

## 🔐 Security

All API endpoints are secured with API key authentication. Include your API key in request headers:

```sh
curl -H "X-API-Key: your-api-key-here" https://sociam.runasp.net/api/v1/auth/register
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
