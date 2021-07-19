using System.ComponentModel.DataAnnotations;

namespace ZkTest.Enum
{
    public enum SdkType
    {
        [Display(Name = "بایو استار ورژن 1")]
        BioStarV1 = 1,
        [Display(Name = "بایو استار ورژن 2")]
        BioStarV2 = 2
    }
}