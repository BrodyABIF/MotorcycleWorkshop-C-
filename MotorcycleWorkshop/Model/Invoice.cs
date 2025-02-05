namespace MotorcycleWorkshop.model;

/// <summary>
/// for simplicity not in DB
/// </summary>
public class Invoice
{
    public string CustomerId { get; set; } = null!;
    public decimal TotalPrice { get; set; }

    public Dictionary<string, decimal> Positions { get; set; } = new();
}