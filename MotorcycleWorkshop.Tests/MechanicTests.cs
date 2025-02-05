using Microsoft.EntityFrameworkCore;
using MotorcycleWorkshop.Infrastructure;
using MotorcycleWorkshop.model;

namespace MotorcycleWorkshop
{
    public class MechanicTests
    {
        [Fact]
        public void CalculateTotalHours_ShouldReturnCorrectHours()
        {
            var mechanic = (Mechanic)Activator.CreateInstance(typeof(Mechanic), true)!;
            var repair1 = (Repair)Activator.CreateInstance(typeof(Repair), true)!;
            var repair2 = (Repair)Activator.CreateInstance(typeof(Repair), true)!;

            mechanic.Name = "Mechanic A";
            mechanic.Email = "mechanic@example.com";
            mechanic.PhoneNumber = "0123456789";
            mechanic.Certification = "Certified Mechanic";

            repair1.RepairDate = new DateTime(2023, 1, 10);
            repair2.RepairDate = new DateTime(2023, 1, 20);

            repair1.Mechanic = mechanic;
            repair2.Mechanic = mechanic;

            mechanic.Repairs.Add(repair1);
            mechanic.Repairs.Add(repair2);

            var totalHours = mechanic.CalculateTotalHours(new DateTime(2023, 1, 1), new DateTime(2023, 1, 31));

            Assert.Equal(2, totalHours);
        }

        [Fact]
        public void GetAvailableMechanics_ShouldReturnMechanicsWithNoRepairsInDateRange()
        {
            var connection = new Microsoft.Data.Sqlite.SqliteConnection("Filename=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<WorkshopDBContext>()
                    .UseSqlite(connection)
                    .Options;

                using var context = new WorkshopDBContext(options);

                context.Database.EnsureDeleted();
                context.Database.Migrate();

                var mechanic1 = (Mechanic)Activator.CreateInstance(typeof(Mechanic), true)!;
                mechanic1.Name = "Mechanic A";
                mechanic1.Email = "mechanicA@example.com";
                mechanic1.Certification = "Certified Mechanic";
                mechanic1.PhoneNumber = "1234567890";

                var mechanic2 = (Mechanic)Activator.CreateInstance(typeof(Mechanic), true)!;
                mechanic2.Name = "Mechanic B";
                mechanic2.Email = "mechanicB@example.com";
                mechanic2.Certification = "Senior Mechanic";
                mechanic2.PhoneNumber = "0987654321";

                var customer = (Customer)Activator.CreateInstance(typeof(Customer), true)!;
                customer.Name = "Customer X";
                customer.Email = "customerX@example.com";
                customer.PhoneNumber = "0123456789";

                context.Customers.Add(customer);
                context.Mechanics.Add(mechanic1);
                context.Mechanics.Add(mechanic2);
                context.SaveChanges();

                var repair = (Repair)Activator.CreateInstance(typeof(Repair), true)!;
                repair.RepairDate = new DateTime(2023, 1, 10);
                repair.Customer = customer;
                repair.Mechanic = mechanic1;

                mechanic1.Repairs.Add(repair);
                context.Repairs.Add(repair);
                context.SaveChanges();

                var availableMechanics = mechanic2.GetAvailableMechanics(
                    new DateTime(2023, 1, 1),
                    new DateTime(2023, 1, 31),
                    context).ToList();

                Assert.Contains(mechanic2, availableMechanics);
                Assert.DoesNotContain(mechanic1, availableMechanics);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}