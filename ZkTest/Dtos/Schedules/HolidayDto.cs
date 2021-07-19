using System;
using System.Collections.Generic;

namespace ZkTest.Dtos {
    public class HolidayGroupDto
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public List<HolidayDto> Holidays { get; set; }
    }

    public class HolidayDto
    {
        public DateTime Date { get; set; }
        public byte Recurrence { get; set; }
    }
}