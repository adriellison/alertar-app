using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AlertarApp.Application.DTOs.User;
using AlertarApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AlertarApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _userService.GetUserByDocumentIdAsync(createUserDto.DocumentId);
            if (existingUser != null)
                return Conflict("Já existe um usuário com este CPF");

            existingUser = await _userService.GetUserByEmailAsync(createUserDto.Email);
            if (existingUser != null)
                return Conflict("Já existe um usuário com este e-mail");

            var user = await _userService.CreateUserAsync(createUserDto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.AuthenticateAsync(loginDto);
            if (user == null)
                return Unauthorized("CPF ou PIN inválidos");

            var token = GenerateJwtToken(user);
            return Ok(new { token, user });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();
            if (userId != id && !User.IsInRole("Admin")) // Somente o próprio usuário ou admin pode ver
                return Forbid();

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto updateUserDto)
        {
            var userId = GetUserId();
            if (userId != id && !User.IsInRole("Admin"))
                return Forbid();

            if (id != updateUserDto.Id)
                return BadRequest("ID na URL e no corpo da requisição não correspondem");

            var user = await _userService.UpdateUserAsync(updateUserDto);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordDto changePasswordDto)
        {
            var userId = GetUserId();
            if (userId != id && !User.IsInRole("Admin"))
                return Forbid();

            var result = await _userService.ChangePasswordAsync(id, changePasswordDto.OldPin, changePasswordDto.NewPin);
            if (!result)
                return BadRequest("PIN atual incorreto");

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            if (userId != id && !User.IsInRole("Admin"))
                return Forbid();

            var result = await _userService.DeactivateUserAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        private string GenerateJwtToken(UserDto user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }

    public class ChangePasswordDto
    {
        public string OldPin { get; set; }
        public string NewPin { get; set; }
    }
}
