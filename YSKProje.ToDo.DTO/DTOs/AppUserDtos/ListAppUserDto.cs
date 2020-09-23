using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.AppUserDtos
{
   public class ListAppUserDto
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Ad alanı zorunludur.")]
        //[Display(Name = "Ad: ")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Soyad alanı zorunludur.")]
        //[Display(Name = "Soyad: ")]
        public string Surname { get; set; }

        //[Display(Name = "Email: ")]
        public string Email { get; set; }

        //[Display(Name = "Resim: ")]
        public string Picture { get; set; }
    }
}
