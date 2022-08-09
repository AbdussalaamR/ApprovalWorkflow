using MEMOJET.Entities;

namespace MEMOJET.Implementations.Service.EMailService
{
    public interface IMailService
    {
        public void SendEMailAsync(MailRequest mailRequest);
    }
}