using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Concrete.EfCore
{
    public class NoteContext : DbContext
    {
        public NoteContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Note> Notes{ get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().HasKey(x =>x.NoteId);
            modelBuilder.Entity<Note>().HasOne(x => x.Parent).WithMany(x=>x.SubNotes).HasForeignKey(x=>x.ParentId);
            
        }
    }
}
