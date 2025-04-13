using BankCreditSystem.Data;
using BankCreditSystem.Domain.Entities;
using BankCreditSystem.web.Services; // Если используется сервис оценки
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BankCreditSystem.web.Controllers
{
    public class CreditApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICreditRiskService _creditRiskService;

        public CreditApplicationsController(ApplicationDbContext context, ICreditRiskService creditRiskService)
        {
            _context = context;
            _creditRiskService = creditRiskService;
        }

        // Этот метод можно использовать для проверки, что контроллер доступен
        public IActionResult Test()
        {
            Console.WriteLine("Метод Test в CreditApplicationsController вызван");
            return Content("Test passed!");
        }

        // GET: CreditApplications
        public async Task<IActionResult> Index()
        {
            var applications = await _context.CreditApplications
                .Include(a => a.Client)
                .ToListAsync();
            return View(applications);
        }

        // GET: CreditApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var application = await _context.CreditApplications
                .Include(a => a.Client)
                .FirstOrDefaultAsync(m => m.CreditApplicationId == id);
            if (application == null)
                return NotFound();

            return View(application);
        }

        // GET: CreditApplications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CreditApplications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreditApplication application)
        {
            Console.WriteLine("Метод Create в CreditApplicationsController вызван");

            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"{entry.Key}: {error.ErrorMessage}");
                    }
                }
                return View(application);
            }

            try
            {
                // Устанавливаем дату создания заявки и начальный статус
                application.ApplicationDate = DateTime.Now;
                application.Status = "на рассмотрении";

                _context.Add(application);
                await _context.SaveChangesAsync();

                // Если используется сервис оценки кредитоспособности, вызываем его
                var isApproved = await _creditRiskService.AssessCreditApplicationAsync(application);
                application.Status = isApproved ? "одобрено" : "отклонено";

                _context.Update(application);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании заявки: {ex.Message}");
                ModelState.AddModelError("", "При сохранении произошла ошибка. Попробуйте снова.");
                return View(application);
            }
        }

        // GET: CreditApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var application = await _context.CreditApplications.FindAsync(id);
            if (application == null)
                return NotFound();

            return View(application);
        }

        // POST: CreditApplications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreditApplication application)
        {
            if (id != application.CreditApplicationId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(application);

            try
            {
                _context.Update(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(application.CreditApplicationId))
                    return NotFound();
                else
                    throw;
            }
        }

        // GET: CreditApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var application = await _context.CreditApplications
                .Include(a => a.Client)
                .FirstOrDefaultAsync(m => m.CreditApplicationId == id);
            if (application == null)
                return NotFound();

            return View(application);
        }

        // POST: CreditApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.CreditApplications.FindAsync(id);
            if (application != null)
            {
                _context.CreditApplications.Remove(application);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.CreditApplications.Any(e => e.CreditApplicationId == id);
        }
    }
}
