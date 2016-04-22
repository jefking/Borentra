namespace Borentra.Core
{
    using Microsoft.ServiceBus.Notifications;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Threading.Tasks;

    /// <summary>
    /// FROM: http://www.windowsazure.com/en-us/manage/services/notification-hubs/notify-users-aspnet/#create-application
    /// </summary>
    public class NotificationCore
    {
        #region Members
        /// <summary>
        /// Data Connection String
        /// </summary>
        public const string NotificationConnectionString = "NotificationConnectionString";

        /// <summary>
        /// Hub Client
        /// </summary>
        private readonly NotificationHubClient hubClient;
        #endregion

        #region Constructors
        public NotificationCore(string hubName)
        {
            var connection = ConfigurationManager.AppSettings[NotificationConnectionString];
            this.hubClient = NotificationHubClient.CreateClientFromConnectionString(connection, hubName);
        }
        #endregion

        #region Methods
        private async Task Send(Guid userId, string message)
        {
            if (Guid.Empty == userId)
            {
                throw new ArgumentException("userId");
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message");
            }

            try
            {
                var alert = "{\"aps\":{\"alert\":\"" + message + "\"}, \"inAppMessage\":\"" + message + "\"}";

                await this.hubClient.SendAppleNativeNotificationAsync(alert, userId.ToString());
            }
            catch (ArgumentException ex)
            {
                // This is expected when an APNS registration doesn't exist.
            }
        }

        public async Task<RegistrationDescription> Register(Guid userId, string installationId, string deviceToken)
        {
            if (Guid.Empty == userId)
            {
                throw new ArgumentException("userId");
            }

            // Get registrations for the current installation ID.
            var regsForInstId = await hubClient.GetRegistrationsByTagAsync(installationId, 100);


            var updated = false;
            var firstRegistration = true;
            RegistrationDescription registration = null;

            // Check for existing registrations.
            foreach (var registrationDescription in regsForInstId)
            {
                if (firstRegistration)
                {
                    // Update the tags.
                    registrationDescription.Tags = new HashSet<string>() { installationId, userId.ToString() };

                    var iosReg = registrationDescription as AppleRegistrationDescription;
                    iosReg.DeviceToken = deviceToken;
                    registration = await hubClient.UpdateRegistrationAsync(iosReg);

                    updated = true;
                    firstRegistration = false;
                }
                else
                {
                    await hubClient.DeleteRegistrationAsync(registrationDescription);
                }
            }

            if (!updated)
            {
                registration = await hubClient.CreateAppleNativeRegistrationAsync(deviceToken,
                    new string[] { installationId, userId.ToString() });
            }

            // Send out a welcome notification.
            await this.Send(userId, "Thanks for registering");

            return registration;
        }
        #endregion
    }
}