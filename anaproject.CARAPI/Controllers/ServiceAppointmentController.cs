using Microsoft.AspNetCore.Mvc;
using anaproject.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace anaproject.CARAPI.Controllers
{
    [Route("api/[controller]")]
    public class ServiceAppointmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ServiceAppointmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/serviceappointment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceAppointment>>> GetAll()
        {
            return await _context.ServiceAppointments.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        // POST: api/serviceappointment
        [HttpPost]
        public async Task<ActionResult<ServiceAppointment>> Create([FromBody] ServiceAppointment appointment)
        {
            try
            {
                if (appointment == null)
                {
                    return BadRequest(new { error = "Appointment data is null" });
                }

                // Debug: Gelen veriyi logla
                Console.WriteLine($"Received appointment: {System.Text.Json.JsonSerializer.Serialize(appointment)}");

                // Model validation
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    
                    return BadRequest(new { 
                        error = "Validation failed", 
                        details = errors 
                    });
                }

                appointment.CreatedAt = System.DateTime.UtcNow;
                
                _context.ServiceAppointments.Add(appointment);
                await _context.SaveChangesAsync();
                
                return CreatedAtAction(nameof(GetAll), new { id = appointment.Id }, appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, details = ex.ToString() });
            }
        }

        // PUT: api/serviceappointment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var app = await _context.ServiceAppointments.FindAsync(id);
            if (app == null) return NotFound();
            app.Status = status;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/serviceappointment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var app = await _context.ServiceAppointments.FindAsync(id);
            if (app == null) return NotFound();
            _context.ServiceAppointments.Remove(app);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 