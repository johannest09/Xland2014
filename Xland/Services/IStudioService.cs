using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xland.Models;

namespace Xland.Services
{
    public interface IStudioService
    {
        IEnumerable<Studio> GetAllStudios();
        Studio GetStudioByID(int? id);
        void CreateStudio(Studio studio);
        void AttachStudio(Studio studio);
        void EditStudio(Studio studio);
        void DeleteStudio(int id);
        IEnumerable<Studio> GetProjectStudios(int? id);
    }
}
