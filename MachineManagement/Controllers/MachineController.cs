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

       /* [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
          {
              var machine = await _service.GetByIdWithFailuresAsync(id);
              if (machine == null) return NotFound();
              return Ok(machine);
          }*/
    }
}