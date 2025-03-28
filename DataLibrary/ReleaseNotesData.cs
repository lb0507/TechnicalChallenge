using Dapper;
using DataLibrary.Models;
using MySql.Data.MySqlClient;
using Mysqlx;
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
        private static ReleaseNotesModel _model; //maybe if I change this to static my life will be easier
                                                 //Big suprise, I changed it to static and my quality of life doubled immediately


        public async Task<List<ReleaseNotesModel>> LoadReleaseNotes<ReleaseNotesModel, U>(string sql, U parameters, string connectionString)
        {
            //find the user in the database with the matching email, then check if the password matches
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<ReleaseNotesModel>(sql, parameters);
                

                return result.ToList();
            }

        }
        public async Task<List<DeletedNotesModel>> LoadDeletedNotes<DeletedNotesModel, U>(string sql, U parameters, string connectionString)
        {
            //find the user in the database with the matching email, then check if the password matches
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<DeletedNotesModel>(sql, parameters);


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

        public async Task ExecuteSql<T>(string sql, T parameters, string connectionString)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync(); // Ensure the connection is open
                await connection.ExecuteAsync(sql, parameters);


            }
        }
        public void SetNoteByParam(string Notes_GUID, string Application_Name, string Note_Content, string Created_by, DateTime Created_date) 
        {
            //set the last added user to the model
            //this is used for the Confirmation Page
            //I received a null reference exception when I tried to set the model directly
            //so I create a temporary local ReleaseNotesModel object the set the values
            //update: that did not resolve the issue, it is still null in Confirmation.razor
            //This was not an issue previous to imlementing EditContext for the validation check
            //The issue is likely with how _model is storing data

            //Maybe it is not taking the value from local variable temp but rather a reference and when 
            //temp is destroyed it loses the data
            //maybe initializing _model to a value that can be overwritten by the parameters?
            /*There's gotta be a better way of doing this
             * ReleaseNotesModel temp = new ReleaseNotesModel();
            temp.Notes_GUID = Notes_GUID;
            temp.Application_Name = Application_Name;
            temp.Note_Content = Note_Content;
            temp.Created_by = Created_by;
            temp.Created_date = Created_date;
            _model = temp;
             Ok if this one doesn't work I'm changing _model to static
            */
            _model = new ReleaseNotesModel();
            _model.Notes_GUID = Notes_GUID;
            _model.Application_Name = Application_Name;
            _model.Note_Content = Note_Content;
            _model.Created_by = Created_by;
            _model.Created_date = Created_date;
            Console.WriteLine(_model);
            Console.WriteLine(_model.Note_Content);

        }

        public void SetNote(ReleaseNotesModel releaseNotesModel)
        {
            _model = releaseNotesModel;
        }
        public ReleaseNotesModel GetLastAdded() 
        {
            try
            {
                return _model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
             
        }
        //get the release note that was just added so we can display the info in the confirmation page
        //the method that calls this will set the model back to null 
        // i.e. SetNote(null)
    }
}
