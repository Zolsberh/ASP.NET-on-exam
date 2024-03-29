﻿using DomainObject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceContext
{ 
    public class RaceCarContext : DbContext
    {
        
        public RaceCarContext(string connectionStr = "DefaultConnection") : base(connectionStr) 
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RaceCarContext>());
        }

        public DbSet<Race> Races { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}
