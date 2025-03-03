namespace Wa.Application.Interfaces;
 public interface INotifierService
    {
        Task NotifyLeadCreationAsync(string email, string leadName);
    }