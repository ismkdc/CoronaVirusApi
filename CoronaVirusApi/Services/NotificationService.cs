using CoronaVirusApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaVirusApi.Services
{
    public interface INotificationService
    {
        Task<bool> Send(string content, Guid UserId);
    }
    public class NotificationService : INotificationService
    {
        private readonly CoronaVirusContext _context;
        public NotificationService(CoronaVirusContext context)
        {
            _context = context;
        }

        public async Task<bool> Send(string content, Guid UserId)
        {
            await _context.Notifications.AddAsync(new Notification
            {
                Content = content,
                UserId = UserId
            });

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
