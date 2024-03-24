using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeMVCcurd.Models;

namespace EmployeeMVCcurd.Data
{
    public class EmployeeMVCcurdContext : DbContext
    {
        public EmployeeMVCcurdContext (DbContextOptions<EmployeeMVCcurdContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeMVCcurd.Models.EmployeeModelView> EmployeeModelView { get; set; } = default!;
    }
}
