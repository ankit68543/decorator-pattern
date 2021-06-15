using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using decorator_design_pattern.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace decorator_pattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersService _playersService;

        public PlayersController(IPlayersService playersService)
        {
            _playersService = playersService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_playersService.GetPlayersList());
        }
    }
}
