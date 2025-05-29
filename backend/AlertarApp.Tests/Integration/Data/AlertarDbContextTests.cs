using System;
using System.Threading.Tasks;
using AlertarApp.Domain.Entities;
using AlertarApp.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AlertarApp.Tests.Integration.Data
{
    public class AlertarDbContextTests : IDisposable
    {
        private readonly AlertarDbContext _context;

        public AlertarDbContextTests()
        {
            // Configurar o banco de dados em memória para testes
            var options = new DbContextOptionsBuilder<AlertarDbContext>()
                .UseInMemoryDatabase(databaseName: $"AlertarTestDb_{Guid.NewGuid()}")
                .Options;

            _context = new AlertarDbContext(options);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task CanAddAndFindUser()
        {
            // Arrange
            var user = new User(
                "Teste da Silva",
                "teste@email.com",
                "(11) 98765-4321",
                "123.456.789-00",
                "hash_do_pin_1234");

            // Act
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Assert
            var foundUser = await _context.Users.FindAsync(user.Id);
            Assert.NotNull(foundUser);
            Assert.Equal(user.Name, foundUser.Name);
            Assert.Equal(user.Email, foundUser.Email);
            Assert.Equal(user.Phone, foundUser.Phone);
            Assert.Equal(user.DocumentId, foundUser.DocumentId);
        }

        [Fact]
        public async Task CanAddAndFindAlert()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var alert = new Alert(
                userId,
                "Socorro",
                "Estou em perigo",
                -23.5505,
                -46.6333);

            // Act
            await _context.Alerts.AddAsync(alert);
            await _context.SaveChangesAsync();

            // Assert
            var foundAlert = await _context.Alerts.FindAsync(alert.Id);
            Assert.NotNull(foundAlert);
            Assert.Equal(alert.Title, foundAlert.Title);
            Assert.Equal(alert.Description, foundAlert.Description);
            Assert.Equal(alert.Latitude, foundAlert.Latitude);
            Assert.Equal(alert.Longitude, foundAlert.Longitude);
            Assert.Equal(userId, foundAlert.UserId);
        }

        [Fact]
        public async Task CanAddAndFindTrustedContact()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var contact = new TrustedContact(
                userId,
                "Contato de Emergência",
                "(11) 98765-4321",
                "contato@email.com",
                true,
                true);

            // Act
            await _context.TrustedContacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            // Assert
            var foundContact = await _context.TrustedContacts.FindAsync(contact.Id);
            Assert.NotNull(foundContact);
            Assert.Equal(contact.Name, foundContact.Name);
            Assert.Equal(contact.Phone, foundContact.Phone);
            Assert.Equal(contact.Email, foundContact.Email);
            Assert.Equal(userId, foundContact.UserId);
            Assert.True(foundContact.SendSms);
            Assert.True(foundContact.SendEmail);
        }
    }
}
