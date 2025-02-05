using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MotorcycleWorkshop.Infrastructure;

namespace MotorcycleWorkshop.model;

public class Mechanic : Person
{
    [MaxLength(20)] public string Certification { get; set; }
    public decimal HourlyRate { get; set; }
    public virtual ICollection<Repair> Repairs { get; private set; } = new List<Repair>();

    public int CalculateTotalHours(DateTime startDate, DateTime endDate)
    {
        return Repairs.Where(r => r.RepairDate >= startDate && r.RepairDate <= endDate).Count();
    }

    public IEnumerable<Mechanic> GetAvailableMechanics(DateTime startDate, DateTime endDate, WorkshopDBContext context)
    {
        var availableMechanics = context.Mechanics
            .Include(m => m.Repairs)
            .Where(m => !m.Repairs.Any(r => r.RepairDate >= startDate && r.RepairDate <= endDate))
            .ToList();

        return availableMechanics;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Mechanic()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}