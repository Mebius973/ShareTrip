using System.Security.Claims;

namespace TripApi.Utils
{
    public class UserUtils
    {
        public static string? getUserId(ClaimsPrincipal user)
        {
            return user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
