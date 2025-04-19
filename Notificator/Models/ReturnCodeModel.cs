namespace Notificator.Models;

public class ReturnCodeModel
{
    public class ReturnRs
    {
        public int ReturnCode { get; set; } = 0;
        public string ReturnMsg { get; set; } = "Post 성공";
    }
}