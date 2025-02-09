﻿namespace Sociam.Application.Helpers;
public static class DomainErrors
{
    public const string FileUploadFailed = "Unable to upload the media.";
    public const string FailedToDeleteMedia = "Failed to delete the media, try again later !!!";

    public static class Following
    {
        public const string CanNotFollowYourself = "You can not follow yourself";
        public const string CanNotUnFollowYourself = "You cannot unfollow yourself.";
        public const string NoFollowing = "No following between them";
    }

    public static class Friendship
    {
        public const string CanNotSendFriendRequestToYourSelf = "Cannot send friend request to yourself";
        public const string PendingFriendRequest = "Friend request already sent";
        public const string BlockedFriendRequest = "Unable to send friend request";
        public const string RejectedFriendRequest = "Your friendship request rejected";
        public const string AlreadyAcceptedFriendRequest = "Users are already friends";
        public const string CanNotRejectFriendRequest = "Can not reject the request, friendship is {0}";
        public const string UndefindFriendRequestStatus = "Invalid friend request status";
        public const string UnableToCreateFriendRequest = "Unable to create friend Request.";
        public const string NotFoundFriendRequest = "Friend request not found";
        public const string UnauthorizedToAcceptFriendRequest = "Not authorized to accept this friend request";
        public const string UnauthorizedToRejectFriendRequest = "You are not authorized to reject this friend request";
        public const string FriendRequestMustBePending = "Friend request must be pending to accept";
        public const string CanNotBeFriendOfYourself = "Can not be a friend of yourself";
        public const string NotFriend = "Viewer with id '{0}' is not a friend with you!";
    }

    public static class Users
    {
        public const string UserNotExists = "User is not exist.";
        public const string UserUnauthorized = "You are not authenticated";

        public const string UnableToCreateAccount =
            "Some errors happened when creating your account, please try again !!";

        public const string UnkownUser = "Unknown User.";
        public const string UnableToUpdateUser = "Unable to Update The User.";
        public const string InvalidAuthCode = "Invalid authentication code.";
        public const string AuthCodeExpired = "Authentication code is expired.";
        public const string AlreadyEmailConfirmed = "Email is cofirmed yet.";
        public const string UserNotFound = "User '{0}' not found.";
        public const string CannotCreateFbUser = "Can not create facebook user.";
        public const string FbFailedAuthentication = "Facebook authentication failed!";
        public const string EmailNotConfirmed = "Email is not confirmed.";
        public const string InvalidCredientials = "Invalid email or password.";
        public const string CodeExpired = "Code has expired. Please request a new reset code.";
        public const string UserHasPrivacy = "User has a privacy setting.";
        public const string UserNotHasPrivacySetting = "User not have privacy settings";
        public const string Invalid2FaCode = "Invalid 2FA Code.";
        public const string InvalidTokenProvider = "Invalid 2FA Token Provider.";

        public const string TwoFactorRequired =
            "Two Factor Authentication Required To Complete Login.";

        public const string TwoFactorAlreadyDisabled = "Two-factor authentication is already disabled for this user.";
        public const string Disable2FaFailed = "Failed to disable two-factor authentication.";

        public const string NoAccessTokenExists = "No access token exists.";

        public const string InvalidVerificationCode = "Invalid verification code.";
    }

    public static class Roles
    {
        public const string ErrorCreatingRole = "Error creating role '{0}'.";
        public const string RoleNotFound = "Role not found: '{0}'.";
        public const string ErrorUpdatingRole = "Error updating role '{0}'.";
        public const string ErrorDeletingRole = "Error deleting role '{0}'.";
        public const string ErrorAssigningRole = "Error assigning role '{0}' to user '{1}'.";
        public const string ErrorAddingClaim = "Error adding claim to {0}'.";
        public const string ErrorAddingClaimToRole = "Error adding claim to '{0}' role.";
    }

    public static class Conversation
    {
        public const string ConversationNotExisted = "Conversation with id '{0}' was not existed";
        public const string ShouldStartConversation = "You should start a conversation first.";
        public const string CanNotStartConversationToSelf = "You can not start conversation to yourself.";
        public const string NoConversationBetweenThem = "There is an existed conversation between {0} and {1}";
    }

    public static class Group
    {
        public const string GroupNotExisted = "Group with id '{0}' was not existed";
        public const string CannotRemoveLastAdmin = "Can not remove last admin";
        public const string ItsMemberYet = "User with id: '{0}' its a member in the group !!!";
        public const string InitGroupConversationFirst = "Start the group conversation first";
        public const string CanNotRemoveYourself = "Can not remove yourself";
        public const string NotMember = "User with id: '{0}' is not a member in the group !!!";
    }

    public static class Messages
    {
        public const string CanNotSendMessagesToSelf = "You can not send messages to yourself.";
        public const string MessageNotFound = "Message not found.";
        public const string MessageNotFoundById = "Message with id '{0}' not found.";
    }

    public static class JoinRequest
    {
        public const string JoinRequestOrGroupNotFound = "Maybe Join request or group not found.";
        public const string NotAllowed = "Not allowed to manage join group requests !!!";
    }

    public static class Story
    {
        public const string StoryNotFounded = "Story with id '{0}' was not founded or active !!!";
        public const string StoryViewedYet = "Story with id '{0}' is already viewed !!!";
    }

    public static class StoryView
    {
        public const string ViewNotFound = "View is not founded!!";
    }
}
