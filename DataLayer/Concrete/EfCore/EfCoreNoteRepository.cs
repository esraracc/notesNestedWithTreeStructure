using DataLayer.Abstract;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Concrete.EfCore
{
    public class EfCoreNoteRepository : EfCoreGenericRepository<Note>, INoteRepository
    {
        public EfCoreNoteRepository(NoteContext context) : base(context)
        {

        }

        private NoteContext Context
        {
            get { return context as NoteContext; }
        }

        public override List<Note> GetAll()
        {
            return Context.Notes.Include(x => x.SubNotes).Where(x => x.IsDeleted == false).ToList();
        }
    }
}
