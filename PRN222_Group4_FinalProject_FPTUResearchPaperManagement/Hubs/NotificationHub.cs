using Microsoft.AspNetCore.SignalR;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Hubs;

public class NotificationHub : Hub
{
    // e.g., public async Task SendMessage(string user, string message)
    public override async Task OnConnectedAsync()
    {
        if (Context.User.IsInRole("Instructor"))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Instructors");
        }

        await base.OnConnectedAsync();
    }
}

