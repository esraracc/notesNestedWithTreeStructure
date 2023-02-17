using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataStructureTree.Models
{
    public class NoteModel
    {
        public NoteDTO Note { get; set; }
        public ICollection<Note> GetAllNotes { get; set; }
        public ICollection<Note> Notes { get; set; }
        public bool IsFirstCall { get; set; }
        public string UserId { get; set; }
    }
}
