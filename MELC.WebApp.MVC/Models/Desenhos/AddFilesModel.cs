namespace MELC.WebApp.MVC.Models.Desenhos
{
    public class AddFilesModel
    {
        public AddFilesModel()
        {
            Files = new List<IFormFile>();
        }
        public Guid DesenhoId { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
