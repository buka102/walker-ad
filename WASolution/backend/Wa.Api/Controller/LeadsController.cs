using Microsoft.AspNetCore.Mvc;
using Wa.Application.Entities;
using Wa.Application.Interfaces;
using AutoMapper;

namespace Wa.Api.Controllers
{
    /// <summary>
    /// Controller for managing leads.
    /// </summary>
    // [Authorize]
    [ApiController]
    [Route("api/leads")]
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _leadService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeadController"/> class.
        /// </summary>
        /// <param name="leadService">The service for managing leads.</param>
        public LeadController(ILeadService leadService, AutoMapper.IMapper mapper)
        {
            _leadService = leadService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all leads.
        /// </summary>
        /// <returns>A list of leads.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Lead>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLeads()
        {
            var leads = await _leadService.GetAllLeadsAsync();
            var result = _mapper.Map<List<LeadDto>>(leads);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new createLead.
        /// </summary>
        /// <param name="createLead">The createLead details.</param>
        /// <returns>The created createLead.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Lead), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateLead([FromBody] CreateLeadDto createLead)
        {
            var createdLead = await _leadService.CreateLeadAsync(createLead);
            return CreatedAtAction(nameof(GetLeads), new { id = createdLead.Id }, createdLead);
        }
    }
}
