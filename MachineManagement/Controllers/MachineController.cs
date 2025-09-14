using MachineManagement.Services;
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
    }
}