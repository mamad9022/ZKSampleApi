
namespace ZkTest.Dtos
{
    public class CreateScheduleDto
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public bool IsDaily { get; set; }
        public DailySchedule DailySchedule { get; set; }

        public WeeklySchedule WeeklySchedule { get; set; }

        public HolidayScheduleDto Holidays { get; set; }
    }
}