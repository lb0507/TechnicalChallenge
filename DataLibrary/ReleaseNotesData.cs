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
       
        public async Task<List<ReleaseNotesModel>> LoadReleaseNotes<ReleaseNotesModel, U>(string sql, U parameters, string connectionString)
        {
            //find the user in the database with the matching email, then check if the password matches
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<ReleaseNotesModel>(sql, parameters);
                

                return result.ToList();
            }

        }
        public async Task SaveNote<T>(string sql, T parameters, string connectionString)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync(); // Ensure the connection is open
                await connection.ExecuteAsync(sql, parameters);


            }
        }
        public void SetLastAdded(ReleaseNotesModel newlyAdded) 
        {
            _model = newlyAdded;
        }
        public ReleaseNotesModel GetLastAdded() { return _model; }
        //public void Refresh() { _model = null; }
    }
}
