namespace Domain;

public class Transaction
{
    public int Id { get; set; } // Unique identifier for the transaction
    public DateTime Date { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public string Message { get; set; }

    // Foreign key to link the transaction to a user
    public string TransactionUserId {get; set;}

    public AppUser TransactionUser { get; set; }
}