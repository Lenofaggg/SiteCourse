//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tec_dep
    {
        public int number_in_dep { get; set; }
        public int id_tec { get; set; }
        public string id_dep { get; set; }
    
        public virtual depot depot { get; set; }
        public virtual technic technic { get; set; }
    }
}
