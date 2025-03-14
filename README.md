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
* **Application Layer**: Implements CQRS (Command and Query Responsibility Segregation) to separate business logic and use cases.
* **Domain Layer**: Defines core entities and domain logic.
* **Infrastructure Layer**: Provides data access and external integrations.
* **Persistence Layer**: Implements repository and unit of work patterns for database operations.
* **Services Layer**: Contains business logic and encapsulates complex operations to keep the application layer clean.

## Features

Here’s a rundown of what Sociam API can do for you! Whether you’re building a social app or just exploring, these features make it easy to create a vibrant, connected community.

### Authentication

Let’s get you signed up and logged in securely:

* Sign up and log in with ease, or use your Facebook or Google account to jump right in.
* Confirm your account to get started.
* Manage your tokens (refresh, revoke, or validate) to keep your sessions secure.
* Forgot your password? No worries—we’ve got a recovery process to reset it.
* Add extra security with Two-Factor Authentication (2FA): enable it, confirm, verify, or disable it as needed.
* Go even further with Multi-Factor Authentication (MFA): enable it, verify, and log in safely.
* Register and authenticate users smoothly to kick off their Sociam journey.
* **API Key Authentication**: Access to the API is secured with an API key, ensuring only authorized users can make requests.

### User Management

Your profile, your rules—here’s how you can manage it:

* Check out your user profile anytime.
* Update your profile info to keep it fresh.
* Change your avatar or cover photo to show off your style.
* Switch up your email (we’ll verify the change for security).
* Update your password whenever you need to.

### Conversation

Start chatting with friends or groups—it’s super simple:

* Kick off a private conversation with someone.
* Grab all the messages from a private chat or group conversation.
* Need to scroll through messages? Get them paged for easier browsing (works for both private and group chats).
* Find a private conversation with a specific user.
* Pull up the chat history between two users.
* Done with a private chat? You can delete it.
* Create a group conversation to bring everyone together.

### Messaging

Messaging is where the magic happens—stay connected in real time:

* Chat with friends instantly using real-time messaging.
* Send a private message to someone directly.
* Check the details of any message.
* Mark a message as read so you don’t lose track.
* Made a typo? Edit your message to fix it.
* Delete a private message between you and another user.
* See how many unread messages you’ve got.
* Grab messages from a specific date range or conversation.
* Check out all your unread messages.
* Search through your messages to find what you need.
* Delete a message in a private chat.
* Get messages with specific details (data shaping lets you pick what you need).
* Reply to a message, whether it’s in a private chat or a group.
* Reply to a specific reply to keep the conversation flowing.
* See the full reply thread for a message (parents and child replies included).

### Groups

Create and manage groups to bring people together:

* **Create a Group**  
  `POST /api/v1/groups`  
  Start a new group for your friends, team, or community.

* **Retrieve All Groups**  
  `GET /api/v1/groups`  
  See a list of all the groups on Sociam.

* **Retrieve Groups by Specific Criteria**  
  `GET /api/v1/groups/by`  
  Find groups that match what you’re looking for (like a search or filter).

* **Retrieve a Specific Group**  
  `GET /api/v1/groups/{groupID}`  
  Check out the details of a specific group using its `groupID`.

* **View Group Information**  
  `GET /api/v1/groups/view`  
  Get a quick summary or overview of a group.

* **Add a User to a Group**  
  `POST /api/v1/groups/{groupID}/users/{userID}`  
  Invite someone (`userID`) to join a group (`groupID`).

* **Join a Group as a Member**  
  `POST /api/v1/groups/{id}/members/join`  
  Jump into a group as a member using the group `id`.

* **Remove a Member from a Group**  
  `DELETE /api/v1/groups/{groupID}/members/{memberID}`  
  Remove a member (`memberID`) from a group (`groupID`) if needed.

* **Update a Member's Role**  
  `PUT /api/v1/groups/{groupID}/members/{memberID}/role`  
  Change a member’s role (like making them an admin) in a group.

* **Manage Group Requests**  
  `PUT /api/v1/groups/{groupID}/requests/{requestID}`  
  Approve or reject a request to join a group (`requestID`) for a group (`groupID`).

* **Send a Message in a Group Conversation**  
  `POST /api/v1/groups/{groupID}/conversations/{conversationID}/messages`  
  Send a message in a group chat (`conversationID`) within a group (`groupID`).

### Posts & Stories

Share your thoughts and moments with the world:

* Create, edit, or delete posts to share what’s on your mind.
* Like and comment on posts to show some love.
* Post stories and interact with others’ stories to keep the vibe going.

### Notifications

Stay in the loop with what’s happening:

* **Get Your Notifications**  
  `GET /api/v1/notifications`  
  See all the notifications waiting for you.

* **Delete All Notifications**  
  `DELETE /api/v1/notifications`  
  Clear out all your notifications in one go.

* **Get a Specific Notification**  
  `GET /api/v1/notifications/{id}`  
  Check the details of a single notification using its `id`.

* **Delete a Specific Notification**  
  `DELETE /api/v1/notifications/{id}`  
  Remove a single notification (`id`) you don’t need anymore.

* **Check Unread Notification Count**  
  `GET /api/v1/notifications/unread-count`  
  Find out how many unread notifications you’ve got.

* **Check Read Notification Count**  
  `GET /api/v1/notifications/read-count`  
  See how many notifications you’ve already read.

* **Mark All Notifications as Read**  
  `PUT /api/v1/notifications/markAll-as-read`  
  Mark all your notifications as read to clear the slate.

* **Mark a Specific Notification as Read**  
  `PUT /api/v1/notifications/{id}/mark-as-read`  
  Mark a single notification (`id`) as read when you’re ready.

### Role Management

Need to manage user roles and permissions? Here’s how admins can handle it:

* **Get All Roles**  
  `GET /api/v1/roles/getAllRoles`  
  See a list of all roles in the system.

* **Get Claims for a Role**  
   `GET /api/v1/roles/getRoleClaims`  
  Check the permissions (claims) tied to a specific role.

* **Get Claims for a User**  
  `GET /api/v1/roles/getUserClaims`  
  See the permissions (claims) assigned to a specific user.

* **Get Roles for a User**  
  `GET /api/v1/roles/getUserRoles`  
  Find out which roles a user has.

* **Add a Claim to a Role**  
  `POST /api/v1/roles/addClaimToRole`  
  Add a new permission (claim) to a role.

* **Assign a Claim to a User**  
  `POST /api/v1/roles/assignClaimToUser`  
  Give a specific permission (claim) to a user.

* **Assign a Role to a User**  
  `POST /api/v1/roles/assignRoleToUser`  
  Assign a role to a user to update their access level.

* **Create a New Role**  
  `POST /api/v1/roles/createRole`  
  Create a new role for your platform.

* **Delete a Role**  
  `POST /api/v1/roles/deleteRole`  
  Remove a role that’s no longer needed.

* **Edit a Role**  
  `POST /api/v1/roles/editRole`  
  Update the details of an existing role.

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

**Note on API Key Authentication**: The Sociam API is secured with API key authentication. To access the endpoints, you must include your API key in the request header. Add the X-API-Key header with your API key (configured in appsettings.json) to authenticate your requests. For example:

```sh
curl -H "X-API-Key: your-api-key-here" https://sociam.runasp.net/api/v1/auth/register
```

## Contact

For any inquiries or issues, please contact the repository owner @NetNinjaEngineer.
