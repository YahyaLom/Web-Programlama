using HastaneOtomasyonASP.NET.Models;
using HastaneOtomasyonASP.NET.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HastaneOtomasyonASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly  UygulamaDbContext _hastaRepository;

        public ApiController(UygulamaDbContext hastaRepository)
        {
            _hastaRepository = hastaRepository;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hasta>>> GetHasta()
        {

            var hasta = await _hastaRepository.Hastalar.ToListAsync();
            return Ok(hasta);
        }

    }


}
