using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        INoteRepository Notes { get; }
        void Save();
    }
}
