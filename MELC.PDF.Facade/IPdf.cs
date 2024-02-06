using MELC.Core.DomainObjects.Dtos;
using MELC.PDF.Facade.Models;

namespace MELC.PDF.Facade
{
    public interface IPdf
    {
        Task<Stream> GeneratePdf(PdfDesenhos pdfDesenhos);
    }
}
