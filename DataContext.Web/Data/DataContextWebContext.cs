using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataContext.Web.Models;

namespace DataContext.Web.Data
{
    public class DataContextWebContext : DbContext
    {
        public DataContextWebContext (DbContextOptions<DataContextWebContext> options)
            : base(options)
        {
        }

        public DbSet<DataContext.Web.Models.MessageModel> MessageModel { get; set; }
    }
}
