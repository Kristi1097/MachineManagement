using MachineManagement.Models;
using MachineManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace MachineManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FailuresController : ControllerBase
    {
        private readonly IFailureService _service;

        public FailuresController(IFailureService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Failure failure)
        {
            var id = await _service.AddFailureAsync(failure);
            return Ok(new { FailureId = id });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var failure = await _service.GetByIdAsync(id);
            if (failure == null) return NotFound();
            return Ok(failure);
        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int offset = 0, [FromQuery] int limit = 10)
        {
            var failures = await _service.GetPagedSortedAsync(offset, limit);
            return Ok(failures);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var failures = await _service.GetAllAsync();
            if (failures == null) return NotFound();
            return Ok(failures);
        }
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromQuery] bool status)
        {
            await _service.ChangeStatusAsync(id, status);
            return Ok(new { Message = "Status updated." });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteFailureAsync(id);
                return Ok(new { Message = "Failure deleted successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
    }
}

