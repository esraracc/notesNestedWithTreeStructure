using DataLayer.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Concrete.EfCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NoteContext _context;
        public UnitOfWork(NoteContext context)
        {
            _context = context;
        }

        private EfCoreNoteRepository _noteRepository;


        public INoteRepository Notes => _noteRepository ??= new EfCoreNoteRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
