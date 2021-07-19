using System.ComponentModel.DataAnnotations;

namespace ZkTest.Enum
{
    public enum TransferLogType
    {
        [Display(Name = "ارسال به دستگاه")]
        Enrollment = 1,
        [Display(Name = "حذف از دستگاه")]
        DeleteUser = 2
    }
}