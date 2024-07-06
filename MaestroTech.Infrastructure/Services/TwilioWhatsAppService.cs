using MaestroTech.Application.Services;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace MaestroTech.Infrastructure.Services
{
    public class TwilioWhatsAppService : IWhatsAppService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromNumber;

        public TwilioWhatsAppService(string accountSid, string authToken, string fromNumber)
        {
            _accountSid = accountSid ?? throw new ArgumentNullException(nameof(accountSid));
            _authToken = authToken ?? throw new ArgumentNullException(nameof(authToken));
            _fromNumber = fromNumber ?? throw new ArgumentNullException(nameof(fromNumber));

            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task SendMessageAsync(string to, string message)
        {
            var messageOptions = new CreateMessageOptions(new PhoneNumber($"whatsapp:{to}"))
            {
                From = new PhoneNumber($"whatsapp:{_fromNumber}"),
                Body = message
            };

            await MessageResource.CreateAsync(messageOptions);
        }
    }
}
