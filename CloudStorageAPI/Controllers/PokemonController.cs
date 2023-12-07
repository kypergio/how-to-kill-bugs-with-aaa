using CloudStorage.Core.Exceptions;
using CloudStorage.Core.Model.DAL;
using CloudStorage.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudStorageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _service;
        public PokemonController(IPokemonService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PokemonUpsertDAL input)
        {
            try
            {
                return Ok(await _service.AddAsync(input));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _service.GetAsync(id));
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
