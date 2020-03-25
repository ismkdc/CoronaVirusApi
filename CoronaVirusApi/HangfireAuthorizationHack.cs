using Hangfire.Dashboard;

namespace CoronaVirusApi
{
    public class HangfireAuthorizationHack : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
