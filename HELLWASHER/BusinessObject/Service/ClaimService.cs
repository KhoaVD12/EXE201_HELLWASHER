using BusinessObject.IService;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BusinessLogicLayer
{
    public class ClaimService : IClaimService
    {
        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            // Fetch the UserId from the claims using the key "UserId"
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");

            GetCurrentUserId = !string.IsNullOrEmpty(userIdClaim) ? int.Parse(userIdClaim) : 0;
        }
        public int GetCurrentUserId { get; }
    }
}
