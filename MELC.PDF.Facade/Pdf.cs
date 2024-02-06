using MELC.Core.Commons;
using MELC.Core.Commons.Enums;
using MELC.Core.Commons.FileHelper;
using MELC.Core.DomainObjects.Dtos;
using MELC.PDF.Facade.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Snippets.Font;
using Font = MigraDoc.DocumentObjectModel.Font;
using Image = MigraDoc.DocumentObjectModel.Shapes.Image;

namespace MELC.PDF.Facade
{
    public class Pdf : IPdf
    {
        private List<string> _filesToDelete { get; set; }
        public Pdf()
        {
            if (PdfSharp.Capabilities.Build.IsCoreBuild)
                GlobalFontSettings.FontResolver = new FailsafeFontResolver();

            _filesToDelete = new List<string>();
        }

        public async Task<Stream> GeneratePdf(PdfDesenhos pdfDesenhos)
        {
            pdfDesenhos.CalcularValoresTotais();

            var document = SetUpDocument();

            SetHeader(pdfDesenhos.Cliente, document.LastSection, pdfDesenhos.LogoImagePath, pdfDesenhos.Pedido.Title,
                pdfDesenhos.ValorTotal);
            
            SetFooter(document.LastSection);

            BuildPecas(pdfDesenhos, document);

            var pdfStream = GetPdfStream(document);

            DeleteFiles();
            
            return pdfStream;
        }

        private void DeleteFiles()
        {
            try
            {
                foreach (var file in _filesToDelete)
                    File.Delete(file);
            }
            catch { }
        }

        private void BuildPecas(PdfDesenhos pdfDesenhos, Document document)
        {
            var paragraph = document.LastSection.AddParagraph("Peças", "Heading2");
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Font.Bold = true;

            foreach (var desenho in pdfDesenhos.Desenhos.OrderBy(x => x.Created))
            {
                string? tituloPeca;

                if (!string.IsNullOrEmpty(desenho.Conjunto) || desenho.NumeroConjunto.HasValue)
                    tituloPeca = $"{desenho.Title} - {desenho.Conjunto} {desenho.NumeroConjunto}";
                else
                    tituloPeca = $"{desenho.Title}";

                var paragraphDesenho = document.LastSection.AddParagraph(tituloPeca, "Heading3");
                paragraphDesenho.Format.Font.Size = 12;
                paragraphDesenho.Format.Font.Bold = true;
                paragraphDesenho.Format.SpaceBefore = Unit.FromCentimeter(0.5);

                InsertImage(document, desenho);
                InserirPecas(document, desenho);
            }
        }

        private void InsertImage(Document document, DesenhoDto desenho)
        {
            var arquivoPeca = desenho.Arquivos.Where(x => x.Extensao != ArquivoExtensao.Pdf.GetDescription());

            if (arquivoPeca.Any())
            {
                var imagem = arquivoPeca.OrderByDescending(x => x.Created).First();

                var arquivoDesenhoPath = imagem.CaminhoArquivo;

                var imageParagraph = document.LastSection.AddParagraph();

                var imageReducedPath =
                    FileHelper.ChangeImageDpi(arquivoDesenhoPath, 20, 20);

                var image = imageParagraph.AddImage(imageReducedPath);
                imageParagraph.Format.Alignment = ParagraphAlignment.Center;
                SetUpImage(image, 80, 80);

                _filesToDelete.Add(imageReducedPath);
            }
        }
        
        private static void SetUpImage(Image image, int heigth, int width)
        {
            image.Width = Unit.FromPoint(width);
            image.Height = Unit.FromPoint(heigth);
            image.RelativeVertical = RelativeVertical.Line;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Left;
            image.WrapFormat.Style = WrapStyle.TopBottom;
            image.RelativeHorizontal = RelativeHorizontal.Column;
            image.Resolution = 1;
        }

        private static void InserirPecas(Document document, DesenhoDto desenho)
        {
            var table = new Table
            {
                Borders =
                {
                    Width = 0.75
                }
            };

            var column = table.AddColumn(Unit.FromCentimeter(3.1));
            column.Format.Alignment = ParagraphAlignment.Left;

            table.AddColumn(Unit.FromCentimeter(3)).Format.Alignment = ParagraphAlignment.Center;
            table.AddColumn(Unit.FromCentimeter(4.9)).Format.Alignment = ParagraphAlignment.Center;
            table.AddColumn(Unit.FromCentimeter(4.9)).Format.Alignment = ParagraphAlignment.Center;

            var row = table.AddRow();
            var cell = row.Cells[0];
            cell.AddParagraph($"Data");

            cell = row.Cells[1];
            cell.AddParagraph($"Quantidade");

            cell = row.Cells[2];
            cell.AddParagraph($"Valor unitário (R$)");
            
            cell = row.Cells[3];
            cell.AddParagraph($"Total (R$)");
            
            row = table.AddRow();
            cell = row.Cells[0];
            cell.AddParagraph(desenho.Created.ToString("d"));

            cell = row.Cells[1];
            cell.AddParagraph(desenho.Quantidade.ToString());

            cell = row.Cells[2];
            var valorUnitario = desenho.Resumo.ValorTotal / desenho.Quantidade;
            cell.AddParagraph(valorUnitario.ToString("N"));
            
            cell = row.Cells[3];
            cell.AddParagraph(desenho.Resumo.ValorTotal.ToString("N")).Format.Font.Bold = true;
            
            table.Rows.LeftIndent = Unit.FromCentimeter(0.02);
            
            document.LastSection.Add(table);
        }
        
        private static async Task EscreveArquivoPdfTeste(Stream pdfStream)
        {
            using (var file = File.Create(Path.Combine(Directory.GetCurrentDirectory(), $"{Guid.NewGuid()}.pdf")))
            {
                await pdfStream.CopyToAsync(file);
            }
        }

        private static MemoryStream GetPdfStream(Document document)
        {
            var pdfRenderer = new PdfDocumentRenderer
            {
                Document = document,
                PdfDocument = new PdfDocument()
            };

            pdfRenderer.RenderDocument();

            var stream = new MemoryStream();
            pdfRenderer.PdfDocument.Save(stream);

            return stream;
        }

        private static Document SetUpDocument()
        {
            var document = new Document();
            var style = document.Styles[StyleNames.Normal]!;
            style.Font.Name = "Calibri";

            var section = document.AddSection();

            section.PageSetup.TopMargin = "6cm";
            section.PageSetup.BottomMargin = "3cm";

            return document;
        }

        private static void SetHeader(ClienteDto client, Section section, string imagePath, string pedidoNome, 
            decimal valorTotal)
        {
            // Add a paragraph to the section
            var image = section.Headers.Primary.AddImage(imagePath);
            SetUpImage(image, 50, 50);

            Table table = new Table();
            table.Borders = null;

            Column column = table.AddColumn(Unit.FromCentimeter(10));
            column.Format.Alignment = ParagraphAlignment.Left;

            table.AddColumn(Unit.FromCentimeter(10));

            table.AddRow();

            var row = table.AddRow();
            var cell = row.Cells[0];
            AddLayoutItem("Cliente  ", client.Nome.ToUpper(), cell);

            cell = row.Cells[1];
            AddLayoutItem("CNPJ  ", client.Cnpj, cell);

            row = table.AddRow();
            cell = row.Cells[0];
            AddLayoutItem("Endereço  ", $"{client.Endereco.Rua}, {client.Endereco.Numero}", cell);

            cell = row.Cells[1];
            AddLayoutItem("Cidade  ", client.Endereco.Cidade, cell);

            row = table.AddRow();
            cell = row.Cells[0];
            AddLayoutItem("Pedido  ", pedidoNome, cell);

            cell = row.Cells[1];
            AddLayoutItem("Data  ", DateTime.Now.ToString("d"), cell);
            
            row = table.AddRow();
            cell = row.Cells[0];
            AddLayoutItem($"Valor total: R$ {valorTotal:N}", string.Empty , cell);

            section.Headers.Primary.Add(table);

            var separatorLine = section.Headers.Primary.AddParagraph();

            separatorLine.Format.Alignment = ParagraphAlignment.Center;
            separatorLine.Format.Borders.Bottom = new Border() { Width = "1pt", Color = Colors.DarkGray };
        }

        private static void SetFooter(Section section)
        {
            var separatorLine = section.Footers.Primary.AddParagraph();

            separatorLine.Format.Alignment = ParagraphAlignment.Center;
            separatorLine.Format.Borders.Bottom = new Border() { Width = "1pt", Color = Colors.DarkGray };

            Table table = new Table();
            table.Borders = null;

            Column column = table.AddColumn(Unit.FromCentimeter(10));
            column.Format.Alignment = ParagraphAlignment.Left;

            table.AddColumn(Unit.FromCentimeter(10));

            table.AddRow();

            var row = table.AddRow();
            var cell = row.Cells[0];
            AddLayoutItem("Maiola Máquinas", "", cell);

            row = table.AddRow();
            cell = row.Cells[0];
            AddLayoutItem("", $"Rua 1 de maio, 961", cell);

            cell = row.Cells[1];
            AddLayoutItem("", "Rio dos Cedros - SC", cell);

            section.Footers.Primary.Add(table);
        }

        private static void AddLayoutItem(string key, string value, Cell cell)
        {
            var paragraphCabecalho = cell.AddParagraph($"{key}");
            paragraphCabecalho.Format.Font.Bold = true;
            paragraphCabecalho.AddFormattedText($"{value}", new Font { Bold = false });
        }
    }
}
