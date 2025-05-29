using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AlertarApp.Application.DTOs.TrustedContact;
using AlertarApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlertarApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TrustedContactsController : ControllerBase
    {
        private readonly ITrustedContactService _trustedContactService;

        public TrustedContactsController(ITrustedContactService trustedContactService)
        {
            _trustedContactService = trustedContactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var contacts = await _trustedContactService.GetTrustedContactsByUserIdAsync(userId);
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();
            var contact = await _trustedContactService.GetTrustedContactByIdAsync(id);
            
            if (contact == null || contact.UserId != userId)
                return NotFound();
                
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTrustedContactDto createTrustedContactDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var contact = await _trustedContactService.CreateTrustedContactAsync(userId, createTrustedContactDto);
            
            return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTrustedContactDto updateTrustedContactDto)
        {
            if (id != updateTrustedContactDto.Id)
                return BadRequest("ID na URL e no corpo da requisição não correspondem");

            var userId = GetUserId();
            var contact = await _trustedContactService.UpdateTrustedContactAsync(userId, updateTrustedContactDto);
            
            if (contact == null)
                return NotFound();
                
            return Ok(contact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var result = await _trustedContactService.DeleteTrustedContactAsync(userId, id);
            
            if (!result)
                return NotFound();
                
            return NoContent();
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            var userId = GetUserId();
            var result = await _trustedContactService.ActivateTrustedContactAsync(userId, id);
            
            if (!result)
                return NotFound();
                
            return NoContent();
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var userId = GetUserId();
            var result = await _trustedContactService.DeactivateTrustedContactAsync(userId, id);
            
            if (!result)
                return NotFound();
                
            return NoContent();
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
