using Microsoft.AspNetCore.Authorization;

namespace Organization.API.Extensions;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string resource, string scope) 
        : base(policy: string.Concat(resource, "#",  scope)) { }
}