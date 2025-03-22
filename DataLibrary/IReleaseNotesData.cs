using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public interface IReleaseNotesData
    {
        Task<List<T>> LoadReleaseNotes<T, U>(string sql, U parameters, string connectionString);

    }
}
