namespace OnlineShopWebAPI.Interface
{
    public interface ISendEmailService
    {
        void BlockedEmailNotification(string emailTo);
        void VerifiedEmailNotification(string emailTo);
    }
}
