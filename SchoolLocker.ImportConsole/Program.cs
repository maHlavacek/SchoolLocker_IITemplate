using SchoolLocker.Persistence;
using System;
using System.Linq;

namespace SchoolLocker.ImportConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Import der Schüler, Spinde und Buchungen in die Datenbank");
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Console.WriteLine("Datenbank löschen");
                unitOfWork.DeleteDatabase();
                Console.WriteLine("Datenbank migrieren");
                unitOfWork.MigrateDatabase();
                Console.WriteLine("Buchungen werden von schoollocker.csv eingelesen");
                var bookings = ImportController.ReadFromCsv().ToArray();
                if (bookings.Length == 0)
                {
                    Console.WriteLine("!!! Es wurden keine Buchungen eingelesen");
                    return;
                }

                Console.WriteLine(
                    $"  Es wurden {bookings.Count()} Buchungen eingelesen, werden in Datenbank gespeichert ...");
                unitOfWork.BookingRepository.AddRange(bookings);
                int countPupils = bookings.GroupBy(b => b.Pupil).Count();
                int countLockers = bookings.GroupBy(b => b.Locker).Count();
                int savedRows = unitOfWork.SaveChanges();
                Console.WriteLine(
                    $"{countLockers} Spinde, {countPupils} Schüler und {savedRows - countLockers - countPupils} Buchungen wurden in Datenbank gespeichert!");
                Console.WriteLine();
                Console.Write("Beenden mit Eingabetaste ...");
                Console.ReadLine();
            }
        }
    }
}
