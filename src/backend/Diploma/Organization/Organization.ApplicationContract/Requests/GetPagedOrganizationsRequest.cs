using Common.API.Paged;

namespace Organization.ApplicationContract.Requests;

public class GetPagedOrganizationsRequest : PagedRequest
{
    public bool IsExternal { get; set; } =  false;
}