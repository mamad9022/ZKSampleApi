using ZkTest.Dtos;
using ZKTest.Result;

namespace ZkTest.Interface
{
    public interface IMatchOnServerService
    {
        Result ServerMatching(BaseDeviceInfoDto baseDeviceInfoDto);

        Result DetachServerMatching(BaseDeviceInfoDto baseDeviceInfoDto);
    }
}