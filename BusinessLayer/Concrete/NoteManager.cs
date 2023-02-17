
using BusinessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NoteManager : INoteService
    {
        private readonly DataLayer.Abstract.IUnitOfWork _unitOfWork;
        public NoteManager(DataLayer.Abstract.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Note entity)
        {
            var result = _unitOfWork.Notes.Create(entity);
            if (result)
                return true;
            return false;
        }

        public bool Delete(Note entity)
        {
            var result = _unitOfWork.Notes.Delete(entity);
            if (result)
                return true;
            return false;
        }

        public List<Note> GetAll()
        {
            return _unitOfWork.Notes.GetAll();
        }

        public Note GetById(int id)
        {
            return _unitOfWork.Notes.GetById(id);
        }

        public bool Update(Note entity)
        {
            var result = _unitOfWork.Notes.Update(entity);
            if (result)
                return true;
            return false;
        }
    }
}
