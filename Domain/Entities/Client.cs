using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankCreditSystem.Domain.Entities
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна")]
        [StringLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов")]
        public string FirstName { get; set; }

        // Поле MiddleName можно оставить необязательным
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Паспортные данные обязательны")]
        [StringLength(20, ErrorMessage = "Паспортные данные не должны превышать 20 символов")]
        public string PassportData { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Доход должен быть положительным числом")]
        public decimal Income { get; set; }

        // Если кредитная история обязательна, добавьте [Required]
        public string CreditHistory { get; set; }

        // Навигационное свойство
        public ICollection<CreditApplication> CreditApplications { get; set; } = new List<CreditApplication>();

    }
}
