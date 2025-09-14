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
    }
}

