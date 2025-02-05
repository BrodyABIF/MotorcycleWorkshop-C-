namespace MotorcycleWorkshop.model;

public class Customer : Person
{
    public virtual ICollection<Repair> Repairs { get; private set; } = new List<Repair>();
    public virtual ICollection<Motorcycle> Motorcycles { get; private set; } = new List<Motorcycle>();

    //-------------------LINQ-Methode TODO
    public IEnumerable<Repair> GetRepairHistory()
    {
        return Repairs.OrderByDescending(r => r.RepairDate).ToList();
    }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Customer()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}