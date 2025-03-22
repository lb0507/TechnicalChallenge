using Dapper;
using DataLibrary.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class ReleaseNotesData : IReleaseNotesData
    {
        private ReleaseNotesModel _model;
        //not static so we don't write to the database during unit testing
        public async Task<List<T>> LoadReleaseNotes<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<T>(sql, parameters);

                return rows.ToList();
            }
        }
    }
}
