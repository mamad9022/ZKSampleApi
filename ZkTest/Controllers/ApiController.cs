using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ZkTest.Dtos;
using ZkTest.Enum;
using ZkTest.Interface;

namespace ZkTest.Controllers
{
    public class ApiController : Controller
    {
        private readonly IDeviceOperationServices _device;

        public ApiController(IDeviceOperationServices device)
        {
            _device = device;
        }

        [HttpGet("clearlog")]
        public IActionResult ClearLog()
        {
            var result = _device.ClearLog();

            if (result.Success == false)
                return result.ApiResult;

            return Ok();
        }

        [HttpGet("alluser")]
        public IActionResult GetAllUser()
        {
            var result = _device.GetAllUser();
            return result.ApiResult;

        }

        [HttpGet("logs")]
        public IActionResult GetAllLog()
        {
            var result = _device.GetLogs();
            return result.ApiResult;

        }

        [HttpPost("connect")]
        public IActionResult Connect(BaseDeviceInfoDto command)
        {
            _device.ConnectDeviceToServer(command.Ip, command.Port);

            return Ok();
        }

        [HttpGet("disconnect")]
        public IActionResult DisConnect()
        {
            _device.Disconnect();

            return Ok();
        }

        [HttpDelete("del-all-user")]
        public IActionResult DellAllUser()
        {
            var result = _device.DeleteAllUser();

            if (result.Success == false)
                return result.ApiResult;

            return Ok();
        }

        [HttpDelete("del-user/{id}")]
        public IActionResult DellUser(int id)
        {
            var result = _device.DeleteUser(id);

            if (result.Success == false)
                return result.ApiResult;

            return Ok();
        }

        [HttpGet("network-config")]
        public IActionResult GetNetworkConfig()
        {
            var result = _device.GetNetWorkConfig();

            if (result.Success == false)
                return result.ApiResult;

            return Ok();
        }

        [HttpGet("get-time")]
        public IActionResult GetTime()
        {
            var result = _device.GetTime();

            if (result.Success == false)
                return result.ApiResult;

            return Ok();
        }

        [HttpGet("get-log-count")]
        public IActionResult GetLogCount()
        {
            var result = _device.LogCount();

            if (result.Success == false)
                return result.ApiResult;

            return Ok();
        }

        [HttpGet("reboot")]
        public IActionResult Reboot()
        {
            var result = _device.RebootDevice();

            if (result.Success == false)
                return result.ApiResult;

            return Ok();
        }

        [HttpPost("set-time")]
        public IActionResult SetTime([FromForm] DateTime date)
        {
            var result = _device.SetTime(date);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();

        }

        [HttpPost("scan-finger")]
        public IActionResult SetFinger([FromForm] int quality, int userId)
        {
            var result = _device.ScanFinger(quality,userId);

            if (result.Success == false)
                return result.ApiResult;

            return result.ApiResult;

        }

        [HttpPost("scan-face")]
        public IActionResult SetFace([FromForm] int quality, int userId)
        {
            var result = _device.ScanFace(quality, userId);

            if (result.Success == false)
                return result.ApiResult;

            return result.ApiResult;

        }

        [HttpPost("set-holiday")]
        public IActionResult SetHoliday([FromForm] int holidayId, int beginMonth, int beginDay, int endmonth, int endday, int timezoneid)
        {
            var result = _device.SetHolidayGroup(holidayId, beginMonth, beginDay, endmonth, endday, timezoneid);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();

        }

        [HttpPost("enroll-user")]
        public IActionResult EnrollUser([FromForm] string code, string name, string password, FingerIndex fingerIndex)
        {
            var result = _device.EnrollUser(code, name, password, fingerIndex);

            if (result.Success == false)
                return result.ApiResult;

            return NoContent();

        }

        [HttpGet("user/{id}")]
        public IActionResult GetUserInfo(int id)
        {
            var result = _device.GetUserInfo(id);

            if (result.Success == false)
                return result.ApiResult;

            return result.ApiResult;

        }
    }
}
