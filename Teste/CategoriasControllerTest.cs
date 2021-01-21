using CursoAPI.Controllers;
using CursoMVC.Models;
using Limilabs.Mail.PDI;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Teste
{
    public class CategoriasControllerTest
    {
        private readonly Mock<DbSet<Categoria>> mockSet;
        private readonly Mock<Contexto> mockContext;
        private readonly Categoria categoria;

        public CategoriasControllerTest()
        {
            mockSet = new Mock<DbSet<Categoria>>();
            mockContext = new Mock<Contexto>();
            categoria = new Categoria { Id = 1, Descricao = "Teste de Categoria" };
            mockContext.Setup(expression: m => m.Categorias.FindAsync(params keyValues: 1))
                .ReturnsAsync(categoria);
        }

        [Fact]
        public async Task GetCategoria()
        {
            var service = new CategoriasController(mockContext.Object);
            var testCategoria = await service.GetCategoria(id:1);
            mockSet.Verify(expression: m => m.FindAsync(params keyValues: 1),
                Times.Once());
            Assert.Equal(expected: categoria, actual: testCategoria);
        }

    }
}
