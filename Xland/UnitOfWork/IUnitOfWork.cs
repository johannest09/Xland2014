using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xland.DAL;

namespace Xland.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        XlandContext DbContext { get; }
        int Save();

    }
}
