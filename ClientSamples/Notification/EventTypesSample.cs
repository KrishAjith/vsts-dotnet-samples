using System.Collections.Generic;
using Microsoft.VisualStudio.Services.Notifications.WebApi;
using Microsoft.VisualStudio.Services.Notifications.WebApi.Clients;
using Microsoft.VisualStudio.Services.WebApi;

namespace Vsts.ClientSamples.Notification
{ 
    /// <summary>
    /// Samples for getting details about available notification event types.
    /// </summary>
    [ClientSample(NotificationApiConstants.AreaName, NotificationApiConstants.EventTypesResource.Name)]
    public class EventTypesSample : ClientSample
    {
        public EventTypesSample(): base()
        {

        }

        public EventTypesSample(ClientSampleContext context): base(context)
        {
        }

        /// <summary>
        /// Returns all event types.
        /// </summary>
        /// <returns></returns>
        [ClientSampleMethod]
        public List<NotificationEventType> GetAllEventTypes()
        {
            VssConnection connection = Context.Connection;
            NotificationHttpClient notificationClient = connection.GetClient<NotificationHttpClient>();

            List<NotificationEventType> eventTypes = notificationClient.ListEventTypesAsync().Result;

            LogEventTypes(eventTypes);

            return eventTypes;
        }

        /// <summary>
        /// Returns the event types that can be used by a custom subscription.
        /// </summary>
        /// <returns></returns>
        [ClientSampleMethod]
        public List<NotificationEventType> GetEventTypesAvailableForCustomSubscriptions()
        {
            VssConnection connection = Context.Connection;
            NotificationHttpClient notificationClient = connection.GetClient<NotificationHttpClient>();

            List<NotificationEventType> eventTypes = notificationClient.ListEventTypesAsync().Result;

            List<NotificationEventType> filteredEventTypes = eventTypes.FindAll(e => {
                return e.CustomSubscriptionsAllowed;
            });

            LogEventTypes(filteredEventTypes);

            return filteredEventTypes;
        }

        private void LogEventTypes(IEnumerable<NotificationEventType> eventTypes)
        {
            int index = 1;
            foreach (var eventType in eventTypes)
            {
                Context.Log("{0}. {1} (ID: {2})",
                    index,
                    string.IsNullOrEmpty(eventType.Name) ? "Unnamed" : eventType.Name,
                    eventType.Id);

                index++;
            }
        }
    }

}