using Microsoft.AspNetCore.Mvc.Filters;

namespace CoronaVirusApi.Attributes
{
    public class NeedRoleAttribute : ActionFilterAttribute
    {
        public string Role { get; set; }
    }
}
