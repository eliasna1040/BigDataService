using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BigDataContext : DbContext
    {
        public DbSet<Feature> Features {  get; set; }
        public DbSet<Geometry> Geometries { get; set; }
        public DbSet<Properties> Properties { get; set; }
        public DbSet<DataSet> DataSets { get; set; }
        public BigDataContext(DbContextOptions<BigDataContext> options) : base(options)
        {

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}
    }
}
