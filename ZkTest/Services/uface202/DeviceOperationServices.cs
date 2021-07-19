using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using zkemkeeper;
using ZkTest.Dtos;
using ZkTest.Enum;
using ZkTest.Interface;
using ZKTest.Result;

namespace ZkTest.Services.uface202
{
    public class DeviceOperationServices : IDeviceOperationServices
    {

        public zkemkeeper.CZKEMClass objCZKEM = new zkemkeeper.CZKEMClass();
        public Result ActiveImageLog()
        {
            return Result.Failed(new BadRequestObjectResult("not support"));

        }

        public List<DeviceInfo> BroadCastSearch()
        {
            throw new NotImplementedException();
        }

        public Result ClearLog()
        {
            ConnectDeviceToServer("192.168.1.201", 4370);
            objCZKEM.ClearGLog(1);

            return Result.SuccessFul();
        }

        public void ConnectDeviceToServer(string ip, int port)
        {
            objCZKEM.Connect_Net(ip, port);
            if (objCZKEM.Connect_Net(ip, port))
            {

            }
            else
            {
                int idwErrorCode = 0;
                objCZKEM.GetLastError(ref idwErrorCode);
                throw new Exception("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString());

            }
        }

        public Result ConnectionStatus()
        {
            return Result.Failed(new BadRequestObjectResult("not support"));

        }

        public Result DeactivateImage()
        {
            return Result.Failed(new BadRequestObjectResult("not support"));

        }

        public Result DeleteAllUser()
        {// in adada chian /?
            ConnectDeviceToServer("192.168.1.201", 4370);

            objCZKEM.ClearData(1, 5);
            //objCZKEM.RefreshData(1);
            return Result.SuccessFul();
        }

        public Result DeleteUser(int userId)
        {
            ConnectDeviceToServer("192.168.1.201", 4370);


            if (!objCZKEM.DeleteUserInfoEx(1, userId))
            {
                return Result.Failed(new BadRequestObjectResult("not support"));

            }
            //objCZKEM.RefreshData(Int32.Parse(baseDeviceInfo.Serial));
            return Result.SuccessFul();
        }

        public Result Disconnect()
        {
            objCZKEM.Disconnect();
            return Result.SuccessFul();
        }

        public Result EnrollUser(string code, string name, string password, FingerIndex fingerIndex)
        {
            ConnectDeviceToServer("192.168.1.201", 4370);
            string Name = string.Empty, Password = string.Empty;
            int Privilege = 0;
            bool Enabled = false;
            if (objCZKEM.SSR_GetUserInfo(1, code, out Name, out Password, out Privilege, out Enabled))
            {
                return Result.Failed(new BadRequestObjectResult("user code is exist"));
            }
            objCZKEM.SSR_SetUserInfo(1, code, name, password, 0, true);
            objCZKEM.StartEnrollEx(code, (int)fingerIndex, 1);
            return Result.SuccessFul();
        }
        public Result FactoryReset()
        {
            return Result.Failed(new BadRequestObjectResult("not support"));

        }

        public Result<List<AccessGroupDto>> GetAccessGroupList()
        {
            throw new NotImplementedException();
        }

        public Result<List<AccessLevelDto>> GetAccessLevel()
        {
            throw new NotImplementedException();
        }

        public Result<List<UserDto>> GetAllUser()
        {
            ConnectDeviceToServer("192.168.1.201", 4370);


            string sdwEnrollNumber = string.Empty, sName = string.Empty, sPassword = string.Empty, sTmpData = string.Empty;
            int iPrivilege = 0, iTmpLength = 0, iFlag = 0, idwFingerIndex;
            bool bEnabled = false;

            List<UserDto> lstFPTemplates = new List<UserDto>();
            List<TemplateDto> temp = new List<TemplateDto>();
            objCZKEM.ReadAllUserID(1);
            objCZKEM.ReadAllTemplate(1);

            while (objCZKEM.SSR_GetAllUserInfo(1, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
            {
                temp.Clear();
                for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                {

                    if (objCZKEM.GetUserTmpExStr(1, sdwEnrollNumber, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))
                    {
                        var tm = new TemplateDto
                        {
                            FingerIndex = (FingerIndex)idwFingerIndex,
                            Image = null,
                            Template = Encoding.ASCII.GetBytes(sTmpData),
                            TemplateData = sTmpData,
                            TemplateType = (TemplateType)iFlag
                        };
                        if (!temp.Contains(tm))
                        {
                            temp.Add(tm);
                        }

                        UserDto fpInfo = new UserDto();
                        fpInfo.AuthMode = 0;
                        fpInfo.CardAuthMode = 0;
                        fpInfo.Name = sName;
                        fpInfo.Code = "";
                        fpInfo.EndDate = DateTime.Now;
                        fpInfo.FaceAuthMode = 0;
                        fpInfo.OperatorLevel = OperatorLevel.User;
                        fpInfo.SecurityLevel = SecurityLevel.Default;
                        fpInfo.StartDate = DateTime.Now;
                        fpInfo.Templates = temp;

                        lstFPTemplates.Add(fpInfo);
                    }
                }

            }
            return Result<List<UserDto>>.SuccessFul(lstFPTemplates);
        }

        public Result<AuthConfigDto> GetAuthConfig()
        {
            return Result<AuthConfigDto>.Failed(new BadRequestObjectResult("not support"));

        }

        public Result<List<DoorDto>> GetDoorList()
        {
            return Result<List<DoorDto>>.Failed(new BadRequestObjectResult("not support"));

        }

        public Result<List<DoorStatusDto>> GetDoorStatus(List<int> doorIds)
        {
            return Result<List<DoorStatusDto>>.Failed(new BadRequestObjectResult("not support"));

        }

        public Result<List<HolidayGroupDto>> GetHolidayGroups()
        {
            return Result<List<HolidayGroupDto>>.Failed(new BadRequestObjectResult("not support"));

        }

        public Result<List<LogInfo>> GetLogs()
        {
            ConnectDeviceToServer("192.168.1.201", 4370);


            string sdwEnrollNumber = "";
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;

            int iFlag;
            string sTmpData = string.Empty;
            int iTmpLength;
            int idwFingerIndex;
            int iGLCount = 0;
            int iIndex = 0;

            int iFaceIndex = 50;//the only possible parameter value
            int iLength = 128 * 1024;//initialize the length(cannot be zero)
            byte[] byTmpData = new byte[iLength];

            objCZKEM.EnableDevice(1, false);
            List<LogInfo> list = new List<LogInfo>();
            if (objCZKEM.ReadGeneralLogData(1))//read all the attendance records to the memory
            //if (axCZKEM1.ReadNewGLogData(iMachineNumber))
            {
                while (objCZKEM.SSR_GetGeneralLogData(1, out sdwEnrollNumber, out idwVerifyMode,
                            out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                {
                    for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                    {
                        if (objCZKEM.GetUserTmpExStr(1, sdwEnrollNumber, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength)
                               ||
                           objCZKEM.GetUserFace(1, sdwEnrollNumber, iFaceIndex, ref byTmpData[0], ref iLength))
                        {
                            string returnValue = string.Empty;
                            objCZKEM.GetSerialNumber(1, out returnValue);
                            var t = new LogInfo
                            {
                                AttendanceType = (AttendanceType)idwVerifyMode,
                                Code = "",
                                DeviceSerial = returnValue,
                                EventTime = DateTime.Parse(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString()),
                                FunctionKey = new byte(),
                                UserId = sdwEnrollNumber,
                                //Image = sTmpData != string.Empty ? Encoding.ASCII.GetBytes(sTmpData)[0] : byTmpData[0],
                                Image = new byte(),
                                ImagePath = ""

                            };
                            list.Add(t);
                        }
                    }
                }
            }
            return Result<List<LogInfo>>.SuccessFul(list);
        }

        public Result<NetworkInfoDto> GetNetWorkConfig()
        {
            ConnectDeviceToServer("192.168.1.201", 4370);


            string IPAddr = "";
            string serialNum = "";

            objCZKEM.GetDeviceIP(1, ref IPAddr);
            objCZKEM.GetSerialNumber(1, out serialNum);
            var network = new NetworkInfoDto
            {
                Ip = IPAddr,
                Serial = serialNum,
            };
            return Result<NetworkInfoDto>.SuccessFul(network);
        }

        public Result<List<ScheduleDto>> GetScheduleList()
        {
            return Result<List<ScheduleDto>>.Failed(new BadRequestObjectResult("not support"));
        }

        public Result<DateTime> GetTime()
        {
            ConnectDeviceToServer("192.168.1.201", 4370);


            int machineNumber = 1;
            int dwYear = 0;
            int dwMonth = 0;
            int dwDay = 0;
            int dwHour = 0;
            int dwMinute = 0;
            int dwSecond = 0;

            bool result = objCZKEM.GetDeviceTime(machineNumber, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute, ref dwSecond);
            DateTime deviceTime = new DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond);

            return Result<DateTime>.SuccessFul(deviceTime);
        }

        public Result<UserDto> GetUserInfo(int userId)
        {
            ConnectDeviceToServer("192.168.1.201", 4370);

            List<TemplateDto> temp = new List<TemplateDto>();
            UserDto fpInfo = new UserDto();
            int iFaceIndex = 50;//the only possible parameter value
            int iLength = 128 * 1024;//initialize the length(cannot be zero)
            byte[] byTmpData = new byte[iLength];
            string sdwEnrollNumber = string.Empty, Name = string.Empty, Password = string.Empty, sTmpData = string.Empty;
            int Privilege = 0, iTmpLength = 0, iFlag = 0, idwFingerIndex = 6;
            bool Enabled = false;
            if (objCZKEM.SSR_GetUserInfo(1, userId.ToString(), out Name, out Password, out Privilege, out Enabled))
            {
                for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                {
                    if (objCZKEM.GetUserTmpExStr(1, userId.ToString(), idwFingerIndex, out iFlag, out sTmpData, out iTmpLength)
                           ||
                       objCZKEM.GetUserFace(1, userId.ToString(), iFaceIndex, ref byTmpData[0], ref iLength))
                    {
                        objCZKEM.GetUserTmpExStr(1, userId.ToString(), idwFingerIndex, out iFlag, out sTmpData, out iTmpLength);
                        temp.Add(new TemplateDto
                        {
                            FingerIndex = (FingerIndex)idwFingerIndex,
                            Image = null,
                            Template = Encoding.ASCII.GetBytes(sTmpData),
                            TemplateData = sTmpData,
                            TemplateType = (TemplateType)iFlag
                        });
                    }
                }

                fpInfo.AuthMode = 0;
                fpInfo.CardAuthMode = 0;
                fpInfo.Name = Name;
                fpInfo.Code = "";
                fpInfo.EndDate = DateTime.Now;
                fpInfo.FaceAuthMode = 0;
                fpInfo.OperatorLevel = OperatorLevel.User;
                fpInfo.SecurityLevel = SecurityLevel.Default;
                fpInfo.StartDate = DateTime.Now;
                fpInfo.Templates = temp;
            }

            return Result<UserDto>.SuccessFul(fpInfo);
        }

        public Result LockDevice()
        {
            return Result.Failed(new BadRequestObjectResult("not support"));

        }

        public Result LockDoor(List<int> doorIds, DoorFlagEnum doorFlag)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));

        }

        public Result<int> LogCount()
        {
            ConnectDeviceToServer("192.168.1.201", 4370);

            int count = 0;
            objCZKEM.GetDeviceStatus(1, 6, ref count);
            return Result<int>.SuccessFul(count);
        }

        public Result RebootDevice()
        {
            ConnectDeviceToServer("192.168.1.201", 4370);

            objCZKEM.RestartDevice(1);
            return Result.SuccessFul();
        }

        public Result ReleaseDoor(List<int> doorIds, DoorFlagEnum doorFlag)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));

        }

        public Result RemoveAccessGroup(List<uint> accessGroupId)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));

        }

        public Result RemoveAccessLevel(List<uint> accessLevelIds)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));

        }

        public Result RemoveDoor(List<int> doorIds)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));

        }

        public Result RemoveHolidayGroup(List<uint> holidayGroupId)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));

        }

        public Result RemoveSchedule(List<uint> scheduleIds)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));

        }

        public Result<ScanResultDto> ScanCard()
        {
            return Result<ScanResultDto>.Failed(new BadRequestObjectResult("not support"));
        }

        public Result<ScanResultDto> ScanFace(int quality, int userId)
        {
            ConnectDeviceToServer("192.168.1.201", 4370);

            int iFaceIndex = 50;//the only possible parameter value
            int iLength = 128 * 1024;//initialize the length(cannot be zero)
            byte[] byTmpData = new byte[iLength];
            if (objCZKEM.GetUserFace(1, userId.ToString(), iFaceIndex, ref byTmpData[0], ref iLength))
            {
                var result = new ScanResultDto
                {
                    Quality = quality,
                    Template = "",
                    TemplateData = byTmpData,
                    TemplateImage = byTmpData
                }; 
                return Result<ScanResultDto>.SuccessFul(result);

            }
            return Result<ScanResultDto>.Failed(new BadRequestObjectResult("not support"));
        }

        public Result<ScanResultDto> ScanFinger(int quality, int userId)
        {
            ConnectDeviceToServer("192.168.1.201", 4370);
            ScanResultDto result = new ScanResultDto();
            string sTmpData = "";
            int iTmpLength = 0;
            int iFlag = 0;
            int idwFingerIndex = 0;
            for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
            {
                if (objCZKEM.GetUserTmpExStr(1, userId.ToString(), idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))
                {
                    result = new ScanResultDto
                    {
                        Quality = quality,
                        Template = sTmpData,
                        TemplateData = Encoding.ASCII.GetBytes(sTmpData),
                        TemplateImage = Encoding.ASCII.GetBytes(sTmpData)
                    };
                }
                return Result<ScanResultDto>.Failed(new BadRequestObjectResult("not support"));
            }
            return Result<ScanResultDto>.SuccessFul(result);

        }

        public DeviceInfo Search()
        {
            throw new NotImplementedException();
        }

        public Result SetAccessGroup(List<CreateAccessGroupDto> createAccessGroup)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));
        }

        public Result SetAccessLevel(List<CreateAccessLevelDto> createAccessLevels)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));
        }

        public Result SetAlarm(List<int> doorIds, DoorAlarmFlagEnum doorAlarmFlag)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));
        }

        public Result SetAuthConfig(SetAuthConfigDto setAuthConfigDto)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));
        }

        public Result SetDoor(CreateDoorDto createDoor)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));
        }

        public Result SetHolidayGroup(int holidayId, int beginMonth, int beginDay, int endmonth, int endday, int timezoneid)
        {
            ConnectDeviceToServer("192.168.1.201", 4370);

            objCZKEM.SSR_SetHoliday(1, 2, beginMonth, beginDay, endmonth, endday, timezoneid);
            return Result.SuccessFul();
        }

        public Result<NetworkInfoDto> SetNetworkConfig(NetworkInfoDto networkInfo)
        {
            return Result<NetworkInfoDto>.Failed(new BadRequestObjectResult("not support"));
        }

        public Result SetSchedule(List<CreateScheduleDto> createSchedules)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));
        }

        public Result<bool> SetTime(DateTime date)
        {
            ConnectDeviceToServer("192.168.1.201", 4370);

            var result = objCZKEM.SetDeviceTime2(1, date.Year, date.Month,
                  date.Day, date.Hour, date.Minute, date.Second);
            return Result<bool>.SuccessFul(result);

        }

        public Result UnlockDevice()
        {
            return Result.Failed(new BadRequestObjectResult("not support"));
        }

        public Result UnlockDoor(List<int> doorIds, DoorFlagEnum doorFlag)
        {
            return Result.Failed(new BadRequestObjectResult("not support"));
        }
    }
}
