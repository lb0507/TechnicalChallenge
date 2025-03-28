using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace DataLibrary
{
    public interface IReleaseNotesData
    {

        Task<List<ReleaseNotesModel>> LoadReleaseNotes<ReleaseNotesModel, U>(string sql, U parameters, string connectionString);
        Task<List<DeletedNotesModel>> LoadDeletedNotes<DeletedNotesModel, U>(string sql, U parameters, string connectionString);
        Task SaveNote<T>(string sql, T parameters, string connectionString);
        //ExecuteSql can be used for multiple functions, i.e. add, delete, etc.
        Task ExecuteSql<T>(string sql, T parameters, string connectionString);
        void SetNoteByParam(string Notes_GUID, string Application_Name, string Note_Content, string Created_by, DateTime Created_date);
        void SetNote(ReleaseNotesModel releaseNotesModel);
        ReleaseNotesModel GetLastAdded();
        
    }
}
