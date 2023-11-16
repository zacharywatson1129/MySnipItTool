using Dapper;
using LaTeXManagerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTeXManagerLibrary
{
    public class SqliteDataConnector : IDataConnector
    {
        public List<LaTeXText> GetFavorites()
        {
            return null;
        }

        public List<LaTeXText> GetLastUsed()
        {/*
            using (System.Data.IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings[“defaultConnection”].ConnectionString))
            {
                return db.Query<LaTeXText>(“Select * From Author”).ToList();
            }*/
            return null;
        }
    }
}
