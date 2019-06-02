using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolLocker.Web.DataTransferObjects
{
    public class BookingDTO
    {
        public int LockerNumber { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public int PupilId { get; set; }
    }
}
