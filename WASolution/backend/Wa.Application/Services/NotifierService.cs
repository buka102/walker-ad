using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Wa.Application.Interfaces;  
    
namespace Wa.Application.Services;

public class NotifierService : INotifierService
{
    private readonly ILogger<NotifierService> _logger;

    public NotifierService(ILogger<NotifierService> logger)
    {
        _logger = logger;
    }

    public async Task NotifyLeadCreationAsync(string? email, string leadName)
    {
        // Simulate async operation
        await Task.Delay(500);
        _logger.LogInformation($"Email and TXT message has been sent to {email} regarding the new lead: {leadName}");
    }
}