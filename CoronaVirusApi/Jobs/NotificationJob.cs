using CoronaVirusApi.Models;
using CoronaVirusApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaVirusApi.Jobs
{
    public class NotificationJob
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        public NotificationJob(IUserService userService, INotificationService notificationService)
        {
            _userService = userService;
            _notificationService = notificationService;
        }

        public async Task Run(string content)
        {
            foreach (var id in await _userService.GetNotificationUserIds())
            {
                await _notificationService.Send(content, id);
            }
        }
    }
}
