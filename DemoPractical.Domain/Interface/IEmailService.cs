using DemoPractical.Models.DTOs;

namespace DemoPractical.Domain.Interface
{
    public interface IEmailService
    {
        Task<bool> SendMail(EmailModel model);
    }
}