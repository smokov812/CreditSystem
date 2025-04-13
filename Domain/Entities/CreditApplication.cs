using System;
using System.ComponentModel.DataAnnotations;

namespace BankCreditSystem.Domain.Entities
{
    public class CreditApplication
    {
        public int CreditApplicationId { get; set; }

        [Required(ErrorMessage = "Необходимо указать клиента")]
        public int ClientId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма кредита должна быть больше нуля")]
        public decimal Amount { get; set; }

        [Range(1, 360, ErrorMessage = "Срок кредита должен быть от 1 до 360 месяцев")]
        public int TermMonths { get; set; }

        [Required(ErrorMessage = "Укажите цель кредита")]
        [StringLength(100, ErrorMessage = "Цель кредита не должна превышать 100 символов")]
        public string Purpose { get; set; }

        public DateTime ApplicationDate { get; set; }

        // Статус будет задаваться программно, поэтому убираем атрибут [Required]:
        public string? Status { get; set; }

        // Навигационные свойства делаем nullable, если они не должны вводиться пользователем
        public Client? Client { get; set; }
        public CreditAssessment? CreditAssessment { get; set; }
    }
}
