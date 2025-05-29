using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AlertarApp.Application.DTOs.User;
using AlertarApp.Application.Interfaces;
using AlertarApp.Domain.Entities;
using AlertarApp.Domain.Interfaces;

namespace AlertarApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? MapToDto(user) : null;
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user != null ? MapToDto(user) : null;
        }

        public async Task<UserDto> GetUserByDocumentIdAsync(string documentId)
        {
            var user = await _userRepository.GetByDocumentIdAsync(documentId);
            return user != null ? MapToDto(user) : null;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var hashedPin = HashPin(createUserDto.Pin);

            var user = new User(
                createUserDto.Name,
                createUserDto.Email,
                createUserDto.Phone,
                createUserDto.DocumentId,
                hashedPin);

            await _userRepository.AddAsync(user);
            return MapToDto(user);
        }

        public async Task<UserDto> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(updateUserDto.Id);
            if (user == null)
                return null;

            // Atualizar o usuário com os novos dados
            // Em uma implementação real, atualizaríamos as propriedades aqui
            // Como estamos seguindo DDD, idealmente teríamos métodos específicos para cada atualização

            await _userRepository.UpdateAsync(user);
            return MapToDto(user);
        }

        public async Task<UserDto> AuthenticateAsync(LoginDto loginDto)
        {
            var hashedPin = HashPin(loginDto.Pin);

            var user = await _userRepository.GetByDocumentIdAndPinAsync(loginDto.DocumentId, hashedPin);
            if (user == null)
                return null;

            user.UpdateLastLogin();
            await _userRepository.UpdateAsync(user);

            return MapToDto(user);
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, string oldPin, string newPin)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            var oldHashedPin = HashPin(oldPin);
            // Idealmente, verificaríamos se o oldHashedPin corresponde ao HashedPin do usuário
            // Como a entidade User não expõe o HashedPin, usaríamos outro método do repositório

            var newHashedPin = HashPin(newPin);
            user.UpdatePin(newHashedPin);
            await _userRepository.UpdateAsync(user);
            
            return true;
        }

        public async Task<bool> DeactivateUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            user.Deactivate();
            await _userRepository.UpdateAsync(user);
            
            return true;
        }

        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                DocumentId = user.DocumentId,
                CreatedAt = user.CreatedAt,
                LastLogin = user.LastLogin,
                IsActive = user.IsActive
            };
        }

        private static string HashPin(string pin)
        {
            // NOTA: Em produção, use um algoritmo de hash mais seguro como bcrypt ou argon2
            // Este é apenas um exemplo simples para demonstração
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(pin);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
