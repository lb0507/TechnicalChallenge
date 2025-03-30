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
        //declare a model to store the data of a release note
        private static ReleaseNotesModel _model; 

        public async Task<List<ReleaseNotesModel>> LoadReleaseNotes<ReleaseNotesModel, U>(string sql, U parameters, string connectionString)
        {
            //a method to load the release notes from the database and return it as a list
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<ReleaseNotesModel>(sql, parameters);
                

                return result.ToList();
            }

        }
        public async Task<List<DeletedNotesModel>> LoadDeletedNotes<DeletedNotesModel, U>(string sql, U parameters, string connectionString)
        {
            //a method to load the deleted release notes from the database and return it as a list
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<DeletedNotesModel>(sql, parameters);


                return result.ToList();
            }

        }
        public async Task SaveNote<T>(string sql, T parameters, string connectionString)
        {// a method to save release notes and deleted release notes to the database
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync(); // Ensure the connection is open
                await connection.ExecuteAsync(sql, parameters);


            }
        }

        public async Task ExecuteSql<T>(string sql, T parameters, string connectionString)
        {// a generic method to execute a sql command, usually used for delete
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync(); // Ensure the connection is open
                await connection.ExecuteAsync(sql, parameters);


            }
        }
        public void SetNoteByParam(string Notes_GUID, string Application_Name, string Note_Content, string Created_by, DateTime Created_date) 
        {
            //set the last added release note to the model
            //this is used for displaying the release not info on the Confirmation Page
            //the model will be set back to null after the info is displayed
            //because of editContext, setting the model directly causes an error, so here we set the properties of the model individually
            _model = new ReleaseNotesModel();
            _model.Notes_GUID = Notes_GUID;
            _model.Application_Name = Application_Name;
            _model.Note_Content = Note_Content;
            _model.Created_by = Created_by;
            _model.Created_date = Created_date;

        }

        public void SetNote(ReleaseNotesModel releaseNotesModel)
        {//a method to set the model to a passed in release note model
            _model = releaseNotesModel;
        }
        public ReleaseNotesModel GetLastAdded()
        {//the method to get the last added release note for the confirmation page
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
    }
}
