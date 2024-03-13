using ASP_Project.Data;
using ASP_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub {
    
    private readonly DataSignalR _datasignalr;
    private readonly DataContext _context;
    private readonly UserManager<AppUser> _userManager;

    public ChatHub(DataSignalR datasignalr, DataContext context, UserManager<AppUser> userManager) {
        _datasignalr = datasignalr;
        _context = context;
        _userManager = userManager;
    }
    
    public async Task JoinSpecificChatRoom(int chatId) {
        var username = _userManager.GetUserName(Context.User);

        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        _datasignalr.connections[Context.ConnectionId] = chatId;
        await Clients.Group(chatId.ToString()).SendAsync("JoinSpecificChatRoom", $"{username} has joined {chatId}");
    }
    
    public async Task SendMessage(string msg) {
        if (_datasignalr.connections.TryGetValue(Context.ConnectionId, out int chatId)) {
            var AppUserId = _userManager.GetUserId(Context.User);
            Console.WriteLine("id = " + AppUserId);

            MessageRecordEntity message = new() 
            {
                Sendtime = DateTime.UtcNow,
                Messagetext = msg,
                ChatRecordEntityId = chatId
            };
            
            var result = await _context.MessageRecordEntities.AddAsync(message);
            if (result != null) {
                await _context.SaveChangesAsync();
            }

            await Clients.Group(chatId.ToString()).SendAsync("ReceiveSpecificMessage", msg);
        }
    }
}