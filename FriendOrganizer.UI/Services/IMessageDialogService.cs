namespace FriendOrganizer.UI.Services
{
    public interface IMessageDialogService
    {
        public MessageDialogResult ShowOkCancelDialog(string text, string title);
    }
}