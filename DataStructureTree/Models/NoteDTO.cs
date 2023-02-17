using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataStructureTree.Models
{
    public class NoteDTO
    {
        public string Content { get; set; }
        public int? ParentId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
