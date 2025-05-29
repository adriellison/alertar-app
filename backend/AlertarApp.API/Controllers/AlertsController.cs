using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AlertarApp.Application.DTOs.Alert;
using AlertarApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlertarApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AlertsController : ControllerBase
    {
        private readonly IAlertService _alertService;

        public AlertsController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var alerts = await _alertService.GetUserAlertsAsync(userId);
            return Ok(alerts);
        }

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearbyAlerts(
            [FromQuery] double latitude, 
            [FromQuery] double longitude, 
            [FromQuery] double radiusInKm = 5.0)
        {
            var alerts = await _alertService.GetNearbyAlertsAsync(latitude, longitude, radiusInKm);
            return Ok(alerts);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAlertDto createAlertDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var alert = await _alertService.CreateAlertAsync(userId, createAlertDto);
            
            return CreatedAtAction(nameof(GetById), new { id = alert.Id }, alert);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var alert = await _alertService.GetAlertByIdAsync(id);
            
            if (alert == null)
                return NotFound();
                
            return Ok(alert);
        }

        [HttpPut("{id}/resolve")]
        public async Task<IActionResult> Resolve(Guid id)
        {
            var userId = GetUserId();
            var result = await _alertService.ResolveAlertAsync(id, userId);
            
            if (!result)
                return NotFound();
                
            return NoContent();
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var userId = GetUserId();
            var result = await _alertService.CancelAlertAsync(id, userId);
            
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
