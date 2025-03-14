# Sociam API
Sociam API is a dynamic and scalable social media platform that enables real-time messaging, group interactions, notifications, and seamless social connections. Designed with modern technologies, it delivers fast, efficient communication and an engaging user experience.

## Technologies
* **ASP.NET Core 9** - Backend framework.
* **Entity Framework Core** - ORM for data access.
* **MS SQL Server** - Database management.
* **SignalR** - Real-time communication.
* **JWT Authentication** - Secure API authentication.
* **Identity Framework** - User management and authentication.
* **AutoMapper** - Object mapping for DTOs.

## Architecture
Sociam API follows a clean and scalable architecture:

* **API Layer**: Handles HTTP requests and responses.
* **Application Layer**: Implements business logic and use cases.
* **Domain Layer**: Defines core entities and domain logic.
* **Infrastructure Layer**: Provides data access and external integrations.
* **Persistence Layer**: Implements repository and unit of work patterns for database operations.

## Features

### User Management
* Register and authenticate users.
* Profile management (avatar, cover, bio, etc.).
* Follow and unfollow users.

### Messaging & Groups
* Real-time messaging between users.
* Group creation and user invitations.
* Fetch message threads and replies.

### Posts & Stories
* Create, edit, and delete posts.
* Like and comment on posts.
* View and interact with stories.

### Notifications
* Push notifications for messages and interactions.
* Event-driven notification system.

## Installation & Setup

1. Clone the repository:
   ```sh
   git clone https://github.com/your-repo/sociam-api.git
   ```
2. Configure `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "YourConnectionStringHere"
     },
     "Jwt": {
       "Key": "YourJwtKeyHere",
       "Issuer": "YourIssuer",
       "Audience": "YourAudience"
     }
   }
   ```
3. Run database migrations:
   ```sh
   dotnet ef database update
   ```
4. Start the application:
   ```sh
   dotnet run
   ```

## API Endpoints
Sociam API follows RESTful conventions. You can explore the API using Swagger:
[Swagger UI](https://sociam.runasp.net/swagger/index.html)

## Contact
For any inquiries or issues, please contact the repository owner @NetNinjaEngineer.
