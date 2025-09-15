using MachineManagement.Models;
using MachineManagement.Services;
using MachineManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MachineManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachinesController : ControllerBase
    {
        private readonly IMachineService _service;

        public MachinesController(IMachineService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Machine machine)
        {
            var id = await _service.AddMachineAsync(machine);
            return Ok(new { MachineId = id });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Machine machine)
        {
            await _service.UpdateMachineAsync(id, machine);
            return Ok(new { Message = "Machine updated." });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var machine = await _service.GetByIdWithFailuresAsync(id);
            if (machine == null) return NotFound();
            return Ok(machine);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var machines = await _service.GetAllAsync();
            if (machines == null) return NotFound();
            return Ok(machines);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteMachineAsync(id);
                return Ok(new { Message = "Machine deleted successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
    }
}