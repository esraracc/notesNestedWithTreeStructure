using BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataLayer.Abstract.IUnitOfWork unitOfWork;
        public UnitOfWork(DataLayer.Abstract.IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private NoteManager _NoteManager;


        public INoteService Notes => _NoteManager ??= new NoteManager(unitOfWork);

    }
}
