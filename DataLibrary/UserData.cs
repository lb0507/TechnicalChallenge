using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;

namespace DataLibrary
{
    public class UserData : IUserData
    {
        private UserModel _model;
        //not static so we don't write to the database during unit testing
        public async Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<T>(sql, parameters);

                return rows.ToList();
            }
        }

        public async Task<bool> LogIn<T, U>(string sql, U parameters, string password, string connectionString)
        {
            //find the user in the database with the matching email, then check if the password matches
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                UserModel result = await connection.QuerySingleAsync<UserModel>(sql, parameters);
                if (result.userpassword == password)
                {
                    //password matches, return user
                    _model = result;
                    return true;
                }
                else
                {
                    //password doesn't match
                    return false;
                }
                
                
            }

        }
        public UserModel GetUserSignOnInfo() { return _model; }

        public void SignOutUser() { _model = null; }
    }
}
