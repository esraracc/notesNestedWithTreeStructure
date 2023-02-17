using BusinessLayer.Abstract;
using DataStructureTree.Models;
using EntityLayer;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataStructureTree.ViewComponents
{
    public class NotesViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotesViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke(ICollection<Note> notes, bool isFirstCall)
        {
            if (isFirstCall) notes = _unitOfWork.Notes.GetAll().ToList();

            var model = new NoteModel
            {
                IsFirstCall = isFirstCall,
                Notes = notes
            };

            return View(model);
        }

    }

}
