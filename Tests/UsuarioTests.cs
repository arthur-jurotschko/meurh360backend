using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediatR;
using meurh360backend.Controllers;
using meurh360backend.Data;
using meurh360backend.Models;
using meurh360backend.Commands;
using meurh360backend.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace meurh360backend.Tests
{
    public class UsuarioTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ApplicationDbContext _context;
        private readonly UsuariosController _controller;

        public UsuarioTests()
        {
            _mediatorMock = new Mock<IMediator>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new UsuariosController(_mediatorMock.Object, _context);
        }


        [Fact]
        public async Task AddUsuario_DeveRetornar_Created()
        {
            var command = new AddUsuarioCommand { Nome = "Teste", Email = "teste@email.com" };
            _mediatorMock.Setup(m => m.Send(command, default))
                .ReturnsAsync(new Usuario { Id = 1, Nome = command.Nome, Email = command.Email });

            var result = await _controller.AddUsuario(command);
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);

            Assert.Equal(1, ((Usuario)actionResult.Value).Id);
        }


        [Fact]
        public async Task GetUsuarioPorId_DeveRetornar_Ok()
        {
            var usuario = new Usuario { Id = 1, Nome = "Teste", Email = "teste@email.com" };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var result = await _controller.GetUsuarioPorId(1);
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(usuario.Id, ((Usuario)actionResult.Value).Id);
        }


        [Fact]
        public async Task UpdateUsuario_DeveRetornar_NoContent()
        {
            var usuario = new Usuario { Id = 1, Nome = "Teste", Email = "teste@email.com" };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var command = new UpdateUsuarioCommand { Id = 1, Nome = "Atualizado", Email = "atualizado@email.com" };
            var result = await _controller.UpdateUsuario(1, command);

            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task DeleteUsuario_DeveRetornar_NoContent()
        {
            var usuario = new Usuario { Id = 1, Nome = "Teste", Email = "teste@email.com" };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteUsuario(1);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
