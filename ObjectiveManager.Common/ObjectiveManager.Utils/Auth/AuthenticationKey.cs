using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ObjectiveManager.Utils.Auth;

public class AuthenticationKey
{
    private const string KeyVariableName = "AppSecurityKey";
    
    public static SecurityKey GetSecurityKey()
    {
        var key = Environment.GetEnvironmentVariable(KeyVariableName);
        if (string.IsNullOrEmpty(key))
        {
            throw new InvalidOperationException("Security key not found.");
        }
        
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
    }
}