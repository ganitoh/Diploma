using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Organization.Infrastructure.Persistance.Context;

namespace Organizaiton.Application.Jobs;

public class OutboxProcessor
{
    private const int _batchSize = 20; 
    
    private readonly ILogger<OutboxProcessor> _logger;
    private readonly OrganizationDbContext _context;

    public OutboxProcessor(ILogger<OutboxProcessor> logger, OrganizationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task GenerateInvoice()
    {
        while (true)
        {
            var messages = await _context.OutboxMessages
                .Where(x => x.ProcessedOnUtc == null)
                .OrderBy(x => x.OccurredOnUtc)
                .Take(_batchSize)
                .ToListAsync();
            
            if (messages.Any())
                break;
            
            foreach (var message in messages)
            {
                
            }
        }
    }
}