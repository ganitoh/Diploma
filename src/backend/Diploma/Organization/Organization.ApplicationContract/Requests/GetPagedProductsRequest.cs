using Common.API.Paged;

namespace Organization.ApplicationContract.Requests;

public class GetPagedProductsRequest  : PagedRequest
{
    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public int? OrganizationId { get; set; }
}