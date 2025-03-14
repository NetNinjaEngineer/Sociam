# Sociam API

Sociam API is a dynamic and scalable social media platform that enables real-time messaging, group interactions, notifications, and seamless social connections. Designed with modern technologies, it delivers fast, efficient communication and an engaging user experience.

## Technologies

* **ASP.NET Core 9** - Backend framework.
* **Entity Framework Core** - ORM for data access.
* **PostgreSQL** - Database management.
* **SignalR** - Real-time communication.
* **JWT Authentication** - Secure API authentication.
* **Identity Framework** - User management and authentication.
* **AutoMapper** - Object mapping for DTOs.
* **FluentValidation** - Validation framework for models.
* **QRCoder** - QR code generation library.

## Architecture

Sociam API follows a clean and scalable architecture:

* **API Layer**: Handles HTTP requests and responses.
* **Application Layer**:  Implements CQRS (Command and Query Responsibility Segregation) to separate business logic and use cases.
* **Domain Layer**: Defines core entities and domain logic.
* **Infrastructure Layer**: Provides data access and external integrations.
* **Persistence Layer**: Implements repository and unit of work patterns for database operations.
* **Services Layer**: Contains business logic and encapsulates complex operations to keep the application layer clean.

## Features

### Authentication

* User Registeration and Login (Including facebook and google login)
* Confirm User Account
* Token Management (Refresh - Revoke - Validate)
* Password Recovery (Forget and Reset)
* Two Factor Authentication 2FA (enable - confirm - verify - disable)
* Multi Factor Authentication MFA (enable - verify - login)
* Register and authenticate users.

### User Management

* Get user profile
* Update user profile information
* Update user avatar profile
* Update user cover profile
* Change account email (change - verify change)
* Change account password

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
   git clone https://github.com/NetNinjaEngineer/Sociam.git
   ```

2. Configure `appsettings.json`:

   ```json
   {
     "ConnectionStrings": {
        "DefaultConnection": "",
        "PostgresConnection": ""
      },
      "JwtSettings": {
        "Key": "",
        "Audience": "",
        "Issuer": "",
        "ExpirationInDays": 1
      }
   }
   ```

3. Add the initial migration
   * Open Package Manager Console (PMC).
   * Select Sociam.Infrastructure as the default project.
   * Run the following command:

   ```sh
   add-migration Initial -o Persistence/Migrations
   ```

4. Run database migrations:

   ```sh
   Update-Database
   ```

5. Start the application:

   ```sh
   dotnet run
   ```

## API Endpoints

Sociam API follows RESTful conventions. You can explore the API using Swagger:
[Swagger UI](https://sociam.runasp.net/swagger/index.html)

## Contact

For any inquiries or issues, please contact the repository owner @NetNinjaEngineer.
