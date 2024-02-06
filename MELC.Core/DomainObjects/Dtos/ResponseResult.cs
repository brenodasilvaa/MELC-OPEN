namespace MELC.Core.DomainObjects.Dtos
{
    public class ResponseResult
    {
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public ResponseErrorMessage Errors { get; set; }
    }

    public class ResponseErrorMessage
    {
        public List<string> Messages { get; set; }
    }
}
