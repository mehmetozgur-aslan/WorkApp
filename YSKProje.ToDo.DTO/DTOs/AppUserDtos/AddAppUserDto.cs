using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.AppUserDtos
{
    public class AddAppUserDto
    {
        //[Required(ErrorMessage = "Kullanıcı Adı boş geçilemez.")]
        //[Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }


        //[Required(ErrorMessage = "Parola boş geçilemez.")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Parola")]
        public string Password { get; set; }


        //[Compare("Password", ErrorMessage = "Parola uyumsuz.")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Parola (Tekrar)")]
        public string ConfirmPassword { get; set; }


        //[Required(ErrorMessage = "Email boş geçilemez.")]
        //[EmailAddress(ErrorMessage = "Uygun bir mail adresi giriniz.")]
        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "Email")]
        public string Email { get; set; }


        //[Required(ErrorMessage = "Ad boş geçilemez.")]
        //[Display(Name = "Ad")]
        public string Name { get; set; }


        //[Required(ErrorMessage = "Soyad boş geçilemez.")]
        //[Display(Name = "Soyad")]
        public string Surname { get; set; }
    }
}
