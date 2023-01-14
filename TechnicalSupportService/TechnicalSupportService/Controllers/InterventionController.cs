
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TechnicalSupportService.Models;
using TechnicalSupportService.Repository;

namespace TechnicalSupportService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterventionController : ControllerBase
    {
       
        private readonly IInterventionRepository InterventionRepository;
      //  private readonly IPublishEndpoint _publishEndpoint;
        public InterventionController(IInterventionRepository InterventionRepository)
        {
            this.InterventionRepository = InterventionRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetInterventions()
        {
            try
            {
                return Ok(await InterventionRepository.GetInterventions());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

       
        [HttpPost]
        public async Task<ActionResult<Intervention>> CreateIntervention(Intervention Intervention)
        {
            try
            {
                if (Intervention == null)
                {
                    return BadRequest();
                }
                else
                // Add custom model validation error
                {
                   
                        var createdIntervention = await InterventionRepository.AddIntervention(Intervention);
                        return CreatedAtAction(nameof(GetIntervention), new { id = createdIntervention.Id },
                        createdIntervention);
                    }
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Intervention>> GetIntervention(int id)
            {
                try
                {
                    var result = await InterventionRepository.GetIntervention(id);
                    if (result == null) return NotFound();
                    return result;
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
                }
            }
        
    }
}

