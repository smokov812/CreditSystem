namespace BankCreditSystem.Domain.Entities
{
    public class CreditAssessment
    {
        public int CreditAssessmentId { get; set; }
        public int CreditApplicationId { get; set; }
        public string AssessmentResult { get; set; }
        public CreditApplication CreditApplication { get; set; }
    }
}
