using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaVirusApi.Attributes;
using CoronaVirusApi.Models.DTO;
using CoronaVirusApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoronaVirusApi.Controllers
{
    [Route("infecteds")]
    [ApiController]
    public class InfectedController : ControllerBase
    {
        private readonly IInfectedService _infectedService;
        public InfectedController(IInfectedService infectedService)
        {
            _infectedService = infectedService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _infectedService.Get(id);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _infectedService.GetAll();

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [NeedRole(Role = "admin")]
        [HttpPost]
        public async Task<IActionResult> Add(InfectedDTO model)
        {
            var result = await _infectedService.Add(model);

            if (result)
                return CreatedAtAction(nameof(Add), null);
            else
                return BadRequest();
        }

        [NeedRole(Role = "admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromQuery]Guid id, [FromBody]InfectedDTO model)
        {
            var result = await _infectedService.Update(id, model);

            if (result)
                return Ok();
            else
                return NotFound();
        }

        [NeedRole(Role = "admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _infectedService.Delete(id);

            if (result)
                return Ok();
            else
                return NotFound();
        }
    }
}