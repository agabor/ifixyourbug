using IFYB.Entities;

namespace IFYB.Models;
public class OrderStateWithMessageDto
{
    public OrderState State { get; set; }
    public Message Message { get; set; }

    public OrderStateWithMessageDto(OrderState state, Message message)
    {
        State = state;
        Message = message;
    }
}
