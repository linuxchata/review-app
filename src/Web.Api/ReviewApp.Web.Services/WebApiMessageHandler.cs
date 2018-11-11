using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ReviewApp.ServiceBusAdapter.Abstractions;
using ReviewApp.Web.Services.Contracts;

namespace ReviewApp.Web.Services
{
    public sealed class WebApiMessageHandler : IMessageHandler
    {
        private readonly ILocationsConverter locationConverter;

        private readonly ILocationService locationService;

        private readonly ILogger<WebApiMessageHandler> logger;

        public WebApiMessageHandler(
            ILocationsConverter locationConverter,
            ILocationService locationService,
            ILogger<WebApiMessageHandler> logger)
        {
            this.locationConverter = locationConverter;
            this.locationService = locationService;
            this.logger = logger;
        }

        public Task Execute(string replyTo, byte[] messageBody)
        {
            this.logger.LogInformation("Message of {Length} B have been received from the message queue", messageBody.Length);

            var locations = this.locationConverter.Convert(messageBody);

            this.logger.LogInformation("Locations have been converted to domain objects");

            return Task.Run(() => { this.locationService.Synchronize(locations); });
        }
    }
}