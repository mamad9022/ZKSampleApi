using System.ComponentModel.DataAnnotations;

namespace ZkTest.Enum
{
    public enum TemplateType
    {
        [Display(Name = "نامشخص")]
        Undefined = 0,

        [Display(Name = "اثر انگشت")]
        Finger = 1,

        [Display(Name = "کارت")]
        Card = 2,

        [Display(Name = "ماشین")]
        Tag = 3,

        [Display(Name = "چهره")]
        Face = 4,

        [Display(Name = "ای دی")]
        Id = 5,

        [Display(Name = "پسورد")]
        Password = 11,
    }
}