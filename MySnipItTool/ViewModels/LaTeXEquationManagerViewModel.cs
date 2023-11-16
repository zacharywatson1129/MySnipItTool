using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaTeXManagerLibrary;
using LaTeXManagerLibrary.Models;

namespace MySnipItTool.ViewModels
{
    public class LaTeXEquationManagerViewModel
    {
        public ObservableCollection<LaTeXText> Equations = new ObservableCollection<LaTeXText>();

        public LaTeXEquationManagerViewModel()
        {
            // Equations = LoadLastUsedEquations();
        }

        /*private ObservableCollection<LaTeXText> LoadLastUsedEquations() 
        { 
            IDataConnector conn = new SqliteDataConnector();
            return conn.GetLastUsed().ForEach(x => Equations.Add(x));
        }*/
    }
}
