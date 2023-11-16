using LaTeXManagerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTeXManagerLibrary
{
    public interface IDataConnector
    {
        List<LaTeXText> GetLastUsed();
        List<LaTeXText> GetFavorites();
    }
}
