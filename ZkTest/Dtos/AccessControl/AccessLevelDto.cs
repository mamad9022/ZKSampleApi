using System.Collections.Generic;

namespace ZkTest.Dtos
{
    public class AccessLevelDto
    {
        public uint Id { get; set; }

        public string Name { get; set; }

        public List<uint> DoorId { get; set; }

        public List<uint> ScheduleId { get; set; }
    }
}