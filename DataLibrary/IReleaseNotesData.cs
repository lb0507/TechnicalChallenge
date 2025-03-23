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
        Task SaveNote<T>(string sql, T parameters, string connectionString);
        void SetLastAdded(ReleaseNotesModel newlyAdded);
        ReleaseNotesModel GetLastAdded();
        //void Refresh();
    }
}
