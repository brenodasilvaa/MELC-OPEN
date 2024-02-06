using MELC.Core.DomainObjects.Dtos;
using MELC.PDF.Facade;
using MELC.PDF.Facade.Models;

var pdf = new Pdf();

var desenhos = new List<DesenhoDto>();

var guid = Guid.NewGuid();

var desenho1 = new DesenhoDto
{
    Quantidade = 3,
    Conjunto = "Mancal",
    NumeroConjunto = 1,
    Arquivos = new List<ArquivoDesenhoDto>
    {
    },
    Pedido = new PedidoDto
    {
        Title = "Manutenção"
    },
    Title = "Eixo",
    MateriaisDesenhos = new List<MaterialDesenhoDto> {
    {
            new MaterialDesenhoDto
            {
                Peso = 12,
                Material = new MaterialDto
                {
                    Nome = "1020",
                    Preco = 12.505m,
                    Densidade = 7.85,
                },
                Quantidade = 1
            }
        }
    },
    PecasNormalizadas = new List<PecaNormalizadaDto> 
    {  
        new PecaNormalizadaDto
        {
            Title = "Parafuso",
            Preco = 1.7,
            Quantidade = 50
        },
        new PecaNormalizadaDto
        {
            Title = "Polias",
            Preco = 2,
            Quantidade = 60
        },
        new PecaNormalizadaDto
        {
            Title = "Arruela",
            Preco = 2,
            Quantidade = 60
        },
        new PecaNormalizadaDto
        {
            Title = "Rolamento",
            Preco = 2,
            Quantidade = 60
        }

    },
    DesenhoServicos = new List<ServicoDesenhoDto>
    {
        new ServicoDesenhoDto
        {
            TipoServicoId = guid,
            Horas = 12,
            TipoServico = new TipoServicoDto
            {
                Id = guid,
                Servico = "Fresa",
                Valor = 100
            }
        },
        new ServicoDesenhoDto
        {
            TipoServicoId = guid,
            Horas = 12,
            TipoServico = new TipoServicoDto
            {
                Id = guid,
                Servico = "Fresa",
                Valor = 100
            }
        },
        new ServicoDesenhoDto
        {
            TipoServicoId = Guid.NewGuid(),
            Horas = 5,
            TipoServico = new TipoServicoDto
            {
                Id = guid,
                Servico = "Torno",
                Valor = 200
            }
        }
    }
};

var desenho2 = new DesenhoDto
{
    Quantidade = 5,
    Arquivos = new List<ArquivoDesenhoDto>
    {
    },
    Pedido = new PedidoDto
    {
        Title = "Manutenção"
    },
    Title = "Mancal",
    PecasNormalizadas = new List<PecaNormalizadaDto>
    {
        new PecaNormalizadaDto
        {
            Title = "Roscas",
            Preco = 2,
            Quantidade = 60
        },
        new PecaNormalizadaDto
        {
            Title = "Polias",
            Preco = 2,
            Quantidade = 60
        },
        new PecaNormalizadaDto
        {
            Title = "Arruela",
            Preco = 2,
            Quantidade = 60
        },
        new PecaNormalizadaDto
        {
            Title = "Rolamento",
            Preco = 2,
            Quantidade = 60
        }

    },
    MateriaisDesenhos = new List<MaterialDesenhoDto> 
    {
        new MaterialDesenhoDto
        {
            MaterialId = guid,
            Peso = 12,
            Material = new MaterialDto
            {
                Id = guid,
                Nome = "1040",
                Preco = 12.505m,
                Densidade = 7.85
            },
            Quantidade = 2
        },
        new MaterialDesenhoDto
        {
            MaterialId = guid,
            Peso = 12,
            Material = new MaterialDto
            {
                Id = guid,
                Nome = "1040",
                Preco = 12.505m,
                Densidade = 7.85
            },
            Quantidade = 2
        },
        new MaterialDesenhoDto
        {
            Peso = 10,
            Material = new MaterialDto
            {
                Id = Guid.NewGuid(),
                Nome = "1050",
                Preco = 12.505m,
                Densidade = 7.85
            },
            Quantidade = 2
        }
    },
    DesenhoServicos = new List<ServicoDesenhoDto>
    {
        new ServicoDesenhoDto
        {
            TipoServicoId = guid,
            Horas = 12,
            TipoServico = new TipoServicoDto
            {
                Id = guid,
                Servico = "Fresa",
                Valor = 100
            }
        },
        new ServicoDesenhoDto
        {
            TipoServicoId = guid,
            Horas = 12,
            TipoServico = new TipoServicoDto
            {
                Id = guid,
                Servico = "Fresa",
                Valor = 100
            }
        },
        new ServicoDesenhoDto
        {
            TipoServicoId = Guid.NewGuid(),
            Horas = 5,
            TipoServico = new TipoServicoDto
            {
                Id = guid,
                Servico = "Torno",
                Valor = 200
            }
        }
    }
};

desenho1.AtualizarResumoFaturamento();
desenho2.AtualizarResumoFaturamento();

desenhos.Add(desenho1);
desenhos.Add(desenho2);

var pdfDesenho = new PdfDesenhos()
{
    LogoImagePath = @"C:\Users\Breno da Silva\Pictures\astrzeneca.jpg",
    Cliente = new ClienteDto { Nome = "Rosina portas", Cnpj = "15215412424", Endereco = new EnderecoDto { Rua = "Ruas das plameiras", Numero = 78, Cidade = "Rio dos cedros"} },
    Desenhos = desenhos,
    Pedido = new PedidoDto()
    {
        Title = "Teste"
    }
};



pdf.GeneratePdf(pdfDesenho);
