using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlertarApp.Application.DTOs.User;

namespace AlertarApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<UserDto> GetUserByDocumentIdAsync(string documentId);
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserDto> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<UserDto> AuthenticateAsync(LoginDto loginDto);
        Task<bool> ChangePasswordAsync(Guid userId, string oldPin, string newPin);
        Task<bool> DeactivateUserAsync(Guid id);
    }
}
