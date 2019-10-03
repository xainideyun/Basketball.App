using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JdCat.Basketball.Model.SeedDatas
{
    public abstract class BaseSeedData
    {
        public ModelBuilder ModelBuilder { get; set; }
        public BaseSeedData(ModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder;
        }
        public abstract void HasData();
    }
}
