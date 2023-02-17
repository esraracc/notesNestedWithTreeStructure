using BusinessLayer.Abstract;
using DataStructureTree.Models;
using EntityLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DataStructureTree.Controllers
{
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new NoteModel
            {
                IsFirstCall = true,
                Notes = _unitOfWork.Notes.GetAll().Where(x => x.ParentId == null).Where(x => x.UserId == user.Id).ToList(),
                GetAllNotes = _unitOfWork.Notes.GetAll().Where(x => x.UserId == user.Id).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteNote(int id)
        {
            var note = _unitOfWork.Notes.GetById(id);
            if (note == null) return NotFound();
            var subNotes = _unitOfWork.Notes.GetAll().Where(x => x.ParentId == note.NoteId).Where(x => x.IsDeleted == false).ToList();
            if (subNotes.Count() > 0)
            {
                for (int i = 0; i < subNotes.Count(); i++)
                {
                    subNotes[i].IsDeleted = true;
                    _unitOfWork.Notes.Update(subNotes[i]);
                }
            }
            note.IsDeleted = true;
            _unitOfWork.Notes.Update(note);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(NoteModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var entity = new Note()
                {
                    Content = model.Note.Content,
                    ParentId = model.Note.ParentId,
                    IsDeleted = false,
                    UserId = user.Id
                };
                _unitOfWork.Notes.Create(entity);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
