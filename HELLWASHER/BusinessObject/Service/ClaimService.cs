using BusinessObject.IService;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BusinessLogicLayer
{
    public class ClaimService : IClaimService
    {
        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            var Id = httpContextAccessor.HttpContext.User?.FindFirstValue("Id");
            GetCurrentUserId = string.IsNullOrEmpty(Id) ? Guid.Empty : Guid.Parse(Id);
        }
        public Guid GetCurrentUserId { get; }
    }
}
