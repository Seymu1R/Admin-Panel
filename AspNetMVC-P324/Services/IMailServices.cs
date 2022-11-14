using AspNetMVC_P324.Data;

namespace AspNetMVC_P324.Services
{
    public interface IMailServices
    {
        public Task SendEmailAsync(RequestEmail requestemail);
    }
}
