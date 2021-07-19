using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZkTest.Dtos;
using ZkTest.Enum;
using ZKTest.Result;

namespace ZkTest.Interface
{
    public interface IDeviceOperationServices
    {
      

        #region Config Device

        Result<DateTime> GetTime();

        Result<bool> SetTime(DateTime date);

        Result ClearLog();

        Result RebootDevice();

        Result FactoryReset();

        Result LockDevice();

        Result UnlockDevice();

        DeviceInfo Search();

        List<DeviceInfo> BroadCastSearch();

        Result ActiveImageLog();

        Result DeactivateImage();

        Result Disconnect();

        #endregion Config Device

        #region log

        Result<List<LogInfo>> GetLogs();

        Result<int> LogCount();


        #endregion

        #region Scan

        Result<ScanResultDto> ScanFinger(int quality, int userId);

        Result<ScanResultDto> ScanCard();

        Result<ScanResultDto> ScanFace(int quality, int userId);

        #endregion Scan

        #region User

        Result<List<UserDto>> GetAllUser();

        Result EnrollUser(string code, string name, string password, FingerIndex fingerIndex);

        Result DeleteAllUser();

        Result DeleteUser(int userId);

        Result<UserDto> GetUserInfo(int userId);

        #endregion User

        #region NetworkConfig

        Result<NetworkInfoDto> GetNetWorkConfig();

        Result<NetworkInfoDto> SetNetworkConfig(NetworkInfoDto networkInfo);

        #endregion NetworkConfig

        #region Door

        Result<List<DoorDto>> GetDoorList();

        Result SetDoor(CreateDoorDto createDoor);

        Result<List<DoorStatusDto>> GetDoorStatus(List<int> doorIds);

        Result RemoveDoor(List<int> doorIds);

        Result ReleaseDoor(List<int> doorIds, DoorFlagEnum doorFlag);

        Result SetAlarm(List<int> doorIds, DoorAlarmFlagEnum doorAlarmFlag);

        Result LockDoor(List<int> doorIds, DoorFlagEnum doorFlag);

        Result UnlockDoor(List<int> doorIds, DoorFlagEnum doorFlag);

        #endregion Door

        #region Access Control

        Result<List<AccessGroupDto>> GetAccessGroupList();

        Result RemoveAccessGroup(List<uint> accessGroupId);

        Result SetAccessGroup(List<CreateAccessGroupDto> createAccessGroup);

        Result<List<AccessLevelDto>> GetAccessLevel();

        Result RemoveAccessLevel(List<uint> accessLevelIds);

        Result SetAccessLevel(List<CreateAccessLevelDto> createAccessLevels);

        Result<List<ScheduleDto>> GetScheduleList();

        Result RemoveSchedule(List<uint> scheduleIds);

        Result SetSchedule(List<CreateScheduleDto> createSchedules);

        public Result<List<HolidayGroupDto>> GetHolidayGroups();

        public Result RemoveHolidayGroup(List<uint> holidayGroupId);

        public Result SetHolidayGroup(int holidayId,int beginMonth,int beginDay,int endmonth,int endday,int timezoneid);

        #endregion Access Control

        #region AuthConfig

        Result<AuthConfigDto> GetAuthConfig();

        Result SetAuthConfig(SetAuthConfigDto setAuthConfigDto);

        #endregion

        void ConnectDeviceToServer(string ip, int port);

        Result ConnectionStatus();
    }
}