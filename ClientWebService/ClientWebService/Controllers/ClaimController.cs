using ClientWebService.DTO;
using ClientWebService.Model;
using ClientWebService.Repository;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Numerics;

namespace ClientWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimRepository claimRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ClaimController (IClaimRepository claimRepository, IPublishEndpoint publishEndpoint)
        {
            this.claimRepository = claimRepository;
            _publishEndpoint = publishEndpoint;

        }

        [HttpGet]
        public async Task<ActionResult> GetClaims()
        {
            try
            {
                return Ok(await claimRepository.GetClaims());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Claim>> GetClaim(int id)
        {
            try
            {
                var result = await claimRepository.GetClaim(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Claim>> CreateClaim(ClaimDto ClaimDto)
        {
            try
            {
                if (ClaimDto == null)
                {
                    return BadRequest();
                }
                else
                    {
                        var createdClaim = await claimRepository.AddClaim(ClaimDto);

                    await _publishEndpoint.Publish<Shared.ClaimCreated>(new Shared.ClaimCreated
                    {
                        ClaimId = createdClaim.ClaimId,
                        Subject = createdClaim.Subject,
                        

                    });
                    return CreatedAtAction(nameof(GetClaim), new { id = createdClaim.ClaimId },
                        createdClaim);
                    }
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("/claims/{clientName}")]
        public async Task<ActionResult> GetClaimsByClient(string clientName)
        {
            try
            {
                return Ok(await claimRepository.GetClaimsByClient(clientName));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        [HttpGet("/claims/product/{productId:int}")]
        public async Task<ActionResult<Product>> GetProductClaim(int productId)
        {
            try
            {
                var result = await claimRepository.getProductClaim(productId);
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
