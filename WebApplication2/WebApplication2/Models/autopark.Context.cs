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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class autoparkEntities : DbContext
    {
        public autoparkEntities()
            : base("name=autoparkEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<city_route> city_route { get; set; }
        public virtual DbSet<depot> depot { get; set; }
        public virtual DbSet<employer> employer { get; set; }
        public virtual DbSet<job_apply> job_apply { get; set; }
        public virtual DbSet<tech_prop> tech_prop { get; set; }
        public virtual DbSet<technic> technic { get; set; }
        public virtual DbSet<emp_dep> emp_dep { get; set; }
        public virtual DbSet<emp_tec_route> emp_tec_route { get; set; }
        public virtual DbSet<tec_dep> tec_dep { get; set; }
    }
}
