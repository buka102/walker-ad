using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wa.Application.Interfaces;
using Wa.Api.Filters;
using Wa.Application.Entities;

namespace Wa.Api.Controllers
{
    /// <summary>
    /// Controller for handling webhooks from third-party services.
    /// </summary>
    // [Authorize]
    [ApiController]
    [Route("api/webhooks/leads")] 
    [ServiceFilter(typeof(PartnersApiKeyAuthorizationFilter))]
    public class ThirdPartyWebhookController : ControllerBase
    {
        private readonly ILogger<ThirdPartyWebhookController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ILeadService _leadService;
        private readonly IMapper _mapper;

        public ThirdPartyWebhookController(
            ILogger<ThirdPartyWebhookController> logger, 
            IConfiguration configuration, 
            IMapper mapper,
            ILeadService leadService)
        {
            _logger = logger;
            _configuration = configuration;
            _leadService = leadService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all leads received via webhooks.
        /// </summary>
        /// <returns>A list of leads.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<Lead>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLeads()
        {
            var leads = await _leadService.GetAllLeadsAsync();
            var result = _mapper.Map<List<LeadDto>>(leads);
            return Ok(result);
        }
        
        /// <summary>
        /// Endpoint for receiving webhook events.
        /// </summary>
        /// <param name="createLead">Incoming createLead data.</param>
        /// <returns>HTTP status indicating success or failure.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Lead), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> ReceiveWebhook([FromBody] CreateLeadDto createLead)
        {
            var createdLead = await _leadService.CreateLeadAsync(createLead);
            _logger.LogInformation("Webhook received successfully: {Lead}", createLead);
            return CreatedAtAction(nameof(ReceiveWebhook), new { id = createdLead.Id }, createdLead);
        }
    }
}
