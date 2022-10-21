using IFYB.Entities;

namespace IFYB.Models;
public class StackoverflowRequestSolvedWithMessageDto
{
    public bool Solved { get; set; }
    public string Message { get; set; }

    public StackoverflowRequestSolvedWithMessageDto(bool solved, string message)
    {
        Solved = solved;
        Message = message;
    }
}
