using BankCreditSystem.Domain.Entities;
using BankCreditSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BankCreditSystem.web.Services
{
    public interface ICreditRiskService
    {
        Task<bool> AssessCreditApplicationAsync(CreditApplication application);
    }

    public class CreditRiskService : ICreditRiskService
    {
        private readonly ApplicationDbContext _context;

        public CreditRiskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AssessCreditApplicationAsync(CreditApplication application)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == application.ClientId);

            if (client != null && client.Income > 50000 && !string.IsNullOrEmpty(client.CreditHistory))
            {
                return true;
            }

            return false;
        }
    }
}
