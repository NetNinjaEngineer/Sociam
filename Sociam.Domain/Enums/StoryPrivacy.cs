namespace Sociam.Domain.Enums;

public enum StoryPrivacy
{
    Public = 1, // Story is available to view for friends or non friends in sociam platform
    Friends, // Friends can view the story
    Custom, // Only specific friends can view the story
    Private // Only the creator can view the story
}