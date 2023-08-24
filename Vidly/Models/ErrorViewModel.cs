namespace Vidly.Models
{
    public class ErrorViewModel
    {
        //donot change this
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}