using Organization.Domain.Enums;

namespace Organization.ApplicationContract.Messages;

public class ChangeStatusOrderMessage
{
    public int OrderId { get; set; }
    public int OrderStatus { get; set; }
}