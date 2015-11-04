using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xrm.Domain.ModelBase
{
    public interface INotify
    {
        void OnPropertyChanged(string propertyName);
    }
}
