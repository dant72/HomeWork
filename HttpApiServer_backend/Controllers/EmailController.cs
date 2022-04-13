using Microsoft.AspNetCore.Mvc;
using WebApplication3;
using HttpApiClient;

namespace HttpApiServer_backend.Controllers
{
    public class EmailController : ControllerBase
    {
        private readonly ISmtpEmailSender _smtpEmailSender;

        public EmailController(ISmtpEmailSender smtpEmailSender)
        {
            _smtpEmailSender = smtpEmailSender;
        }

        [HttpPost]
        public async Task Send([FromBody]MessageInfo message)
        {
            await _smtpEmailSender.Send(message.Email, message.Subject, message.Body, new CancellationToken());
        }
    }
}
