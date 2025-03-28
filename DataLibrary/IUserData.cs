using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace DataLibrary
{
    public interface IUserData
    {
        Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString);
        Task<bool> LogIn<T, U>(string sql, U parameters, string password, string connectionString);
        UserModel GetUserSignOnInfo();
        void SignOutUser();
    }
}
