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
    
    public partial class emp_tec_route
    {
        public string number { get; set; }
        public int id_tec { get; set; }
        public int id_passport { get; set; }
        public string type_e_t { get; set; }
    
        public virtual city_route city_route { get; set; }
        public virtual employer employer { get; set; }
        public virtual technic technic { get; set; }
    }
}
