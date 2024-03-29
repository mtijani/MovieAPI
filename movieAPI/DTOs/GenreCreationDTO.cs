﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace movieAPI.DTOs
{
    public class GenreCreationDTO
    {
        [Required(ErrorMessage = "The field with name {0} is required")]
        [StringLength(50)]
        //  [FirstLetterUpperCase]
        public string Name { get; set; }
    }
}
