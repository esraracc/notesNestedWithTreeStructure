using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Content { get; set; }
        public int? ParentId { get; set; }
        public Note Parent { get; set; }
        public bool IsDeleted { get; set; }
        public List<Note> SubNotes { get; set; }
        public string UserId { get; set; }
    }
}
