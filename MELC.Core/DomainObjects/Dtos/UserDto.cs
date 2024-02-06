namespace MELC.Core.DomainObjects.Dtos
{
    public class UserDto
    {
        public UserDto()
        {
            UserName = string.Empty;
            FullName = string.Empty;
        }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
