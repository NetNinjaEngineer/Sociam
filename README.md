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

* User registeration and login.
* Facebook login.
* Google login.
* Confirm User Account.
* Token Management (Refresh - Revoke - Validate).
* Password Recovery (Forget and Reset).
* Two Factor Authentication 2FA (enable - confirm - verify - disable).
* Multi Factor Authentication MFA (enable - verify - login).
* Register and authenticate users.

### User Management

* Get user profile.
* Update user profile information.
* Update user avatar profile.
* Update user cover profile.
* Change account email (change - verify change).
* Change account password.

### Conversation

* Start private conversation.
* Get conversation messages (private and group).
* Get paged conversation messages (private and group).
* Get private conversation for specific user.
* Get a conversation between two users.
* Delete a private conversation.
* Start a group conversation.

### Messaging

* Real-time messaging between users.
* Send private message to user.
* Get the details of message.
* Mark message as read.
* Edit existing message.
* Delete a private message between two users.
* Get unread messages count for the authenticated user.
* Get the messages by date range or by specific conversation.
* Get unread messages for the authenticated users.
* Search messages.
* Delete a private message in private conversation.
* Get all messages by fields (data shaping).
* Reply to user message either is a private or in a existing group.
* Reply to specific message reply.
* Get the replies tree for message (parent and child replies).

### Groups

- **Create a Group**  
  `POST /api/v1/groups`  
  Allows users to create a new group.

- **Retrieve All Groups**  
  `GET /api/v1/groups`  
  Fetches a list of all groups.

- **Retrieve Groups by Specific Criteria**  
  `GET /api/v1/groups/by`  
  Retrieves groups based on specific criteria (e.g., filters or search parameters).

- **Retrieve a Specific Group**  
  `GET /api/v1/groups/{groupID}`  
  Fetches details of a specific group using its `groupID`.

- **View Group Information**  
  `GET /api/v1/groups/view`  
  Provides a view of group-related information.

- **Add a User to a Group**  
  `POST /api/v1/groups/{groupID}/users/{userID}`  
  Adds a specific user (`userID`) to a group (`groupID`).

- **Join a Group as a Member**  
  `POST /api/v1/groups/{id}/members/join`  
  Allows a user to join a group as a member using the group `id`.

- **Remove a Member from a Group**  
  `DELETE /api/v1/groups/{groupID}/members/{memberID}`  
  Removes a member (`memberID`) from a group (`groupID`).

- **Update a Member's Role**  
  `PUT /api/v1/groups/{groupID}/members/{memberID}/role`  
  Updates the role of a member (`memberID`) in a group (`groupID`).

- **Manage Group Requests**  
  `PUT /api/v1/groups/{groupID}/requests/{requestID}`  
  Handles group requests (approving or rejecting a request to join) identified by `requestID` within a group (`groupID`).

- **Send a Message in a Group Conversation**  
  `POST /api/v1/groups/{groupID}/conversations/{conversationID}/messages`  
  Allows users to send a message in a specific conversation (`conversationID`) within a group (`groupID`).

### Posts & Stories

* Create, edit, and delete posts.
* Like and comment on posts.
* View and interact with stories.

### Notifications

* Push notifications for messages and interactions.
* Event-driven notification system.'

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
