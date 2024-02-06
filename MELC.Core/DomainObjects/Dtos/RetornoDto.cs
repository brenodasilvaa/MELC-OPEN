namespace MELC.Core.DomainObjects.Dtos
{
    public class RetornoDto<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }
}
