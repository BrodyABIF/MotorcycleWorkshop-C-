namespace MotorcycleWorkshop.model;

/// <summary>
/// Aggregate: Root Entity
/// </summary>
public class Repair
{

    public int Id { get; private set; }
    public Guid AlternateId { get; private set; } = Guid.NewGuid();

    private DateTime _repairDate;
    public virtual Customer Customer { get; set; }
    public int CustomerId { get; set; }
    public virtual Mechanic Mechanic { get; set; }
    public int MechanicId { get; set; }
    
    private readonly List<Part> _usedParts = new(); //------------ auto Backing-Field, weil Readonly-Auto-Property TODO
    public virtual IReadOnlyCollection<Part> UsedParts => _usedParts.AsReadOnly();

    public DateTime RepairDate
    {
        get => _repairDate;
        set
        {
            if (value == default)
                throw new ArgumentException("RepairDate cannot be default.");
            _repairDate = value;
        }
    }


    public void AddPart(Part part) // -------- Hinzufügen von Instanzen innerhalb einer Vererbungskette TODO
    {
        if (part == null) throw new ArgumentNullException(nameof(part), "Part cannot be null.");
        _usedParts.Add(part);
    }

    public void RemovePart(int partId) // ----------------- entfernen von Instanzen innerhalb einer Vererbungskette TODO
    {
        var part = _usedParts.FirstOrDefault(p => p.Id == partId);
        if (part != null) _usedParts.Remove(part);
    }

    //---------------- Aggregate Vererbung TODO
    public bool TryDestroyPart(int partId, DestroyedPart.ReasonEnum reason, out DestroyedPart? destroyedPart) //-------------Umwandlung eines Objekts innerhabl Vererbungskette TODO
    {
        destroyedPart = null;
        var part = _usedParts.FirstOrDefault(p => p.Id == partId);

        if (part is not null)
        {
            destroyedPart = new DestroyedPart(reason);
            _usedParts.Remove(part);
            return true;
        }

        return false;
    }

//------------Methode TODO
    public Invoice GetRepairInvoice()
    {
        var invoice = new Invoice
        {
            TotalPrice = _usedParts.Sum(p => p.Price) + Mechanic.HourlyRate,
            Positions = _usedParts.ToDictionary(p => p.Name, p => p.Price),
            CustomerId = Customer.AlternateId.ToString()
        };
        invoice.Positions["Arbeitszeit"] = Mechanic.HourlyRate;
        return invoice;
    }

    public void AssignMechanic(Mechanic newMechanic)
    {
        Mechanic = newMechanic ?? throw new ArgumentNullException(nameof(newMechanic));
    }

    public decimal CalculateTotalCost() => _usedParts.Sum(p => p.Price);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Repair()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}