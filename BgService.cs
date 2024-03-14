
using ASP_Project.Data;

namespace ASP_Project;

public class BgService : BackgroundService
{
    private readonly ILogger<BgService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    // private readonly DataContext _dbContext;


    public BgService(ILogger<BgService> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        // _dbContext = dbContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
            {
                // _logger.LogInformation("Checking for expired chats...");

                using (var scope = _scopeFactory.CreateScope())
                {
                         var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                //      // ค้นหาแชทที่ถึงเวลาตาม endAt
                    var expiredChats = dbContext.ChatEntities
                        .Where(c => c.endAt != null && c.endAt <= DateTime.UtcNow)
                        .ToList();

                    if (expiredChats.Count() > 0)
                    {
                        // ลบแชทที่หมดอายุ
                        dbContext.ChatEntities.RemoveRange(expiredChats);
                        await dbContext.SaveChangesAsync();

                        _logger.LogInformation($"{expiredChats.Count()} expired chats deleted.");
                    }
                    else
                    {
                        _logger.LogInformation("No expired chats found.");
                    }
                // Console.WriteLine("ping");
                // await Task.Delay(1000);
                }
                // await Task.Delay(1000);
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken); // รออีก 1 ชั่วโมงก่อนที่จะตรวจสอบอีกครั้ง
            }
    }
}