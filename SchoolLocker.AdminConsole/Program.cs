using CommandLine;

namespace SchoolLocker.AdminConsole
{
    class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<
                    CreateBookingOptions,
                    RetrieveBookingsOptions,
                    AddLockerOptions,
                    RetrieveLockersOptions,
                    DeleteLockerOptions>(args)
                .MapResult(
                    (CreateBookingOptions opts) => CreateBooking(opts),
                    (RetrieveBookingsOptions opts) => RetrieveBookings(opts),
                    (AddLockerOptions opts) => AddLocker(opts),
                    (RetrieveLockersOptions opts) => RetrieveLockers(opts),
                    (DeleteLockerOptions opts) => DeleteLocker(opts),
                    errs => 1);
        }


        private static int CreateBooking(CreateBookingOptions opts)
        {
            //TODO: Logik zum Erstellen von Buchungen implementieren (inkl. Validierungen!)
            return 0;
        }

        private static int RetrieveBookings(RetrieveBookingsOptions opts)
        {
            //TODO: Logik zum Abfragen von Buchungen implementieren
            return 0;
        }

        private static int AddLocker(AddLockerOptions opts)
        {
            //TODO: Logik zum Hinzufügen eines neuen Spinds implementieren (inkl. Validierungen!)
            return 0;
        }

        private static int RetrieveLockers(RetrieveLockersOptions opts)
        {
            //TODO: Logik zum Abfragen der Spinde implementieren
            return 0;
        }

        private static int DeleteLocker(DeleteLockerOptions opts)
        {
            //TODO: Logik zum Löschen eines Spinds implementieren
            return 0;
        }
    }
}
