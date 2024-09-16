using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Task_manager.Enum
{
    
        public enum Priority    
    {
        [Display(Name = "Простая")]
        Easy = 0,
        [Display(Name = "Важная")]
        Medium = 1,
        [Display(Name = "Критичная")]
        High = 2
    }
}
