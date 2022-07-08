using IFYB.Entities;

namespace IFYB.Models;
public class OrderStateWithMessageDto
{
    public OrderState State { get; set; }
    public string Message { get; set; }

    public OrderStateWithMessageDto(OrderState state, string message)
    {
        State = state;
        Message = message;
    }
}
