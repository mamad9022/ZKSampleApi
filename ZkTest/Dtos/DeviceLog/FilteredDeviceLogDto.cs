using System;
using ZkTest.Dtos;
using ZkTest.Enum;

namespace ZkTest.Dtos
{
    public class FilteredDeviceLogDto : BaseDeviceInfoDto
    {
        public DateTime? FromDate { get; set; }

        public DateTime ToDate { get; set; } = DateTime.Now;

        public long? LastLogId { get; set; }

        public SdkType SdkType { get; set; }

        public bool IsConnected { get; set; }
    }
}