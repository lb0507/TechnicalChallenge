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
        //declare a model to store the user's data
        private UserModel _model;
        
        public async Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString)
        {//a method to load the user's data from the database
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {   //execute the query and return the row with the user's data
                var rows = await connection.QueryAsync<T>(sql, parameters);

                return rows.ToList();
            }
        }

        public async Task<bool> LogIn<T, U>(string sql, U parameters, string password, string connectionString)
        {
            //find the user in the database with the matching email, then check if the password matches
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                UserModel result = await connection.QuerySingleOrDefaultAsync<UserModel>(sql, parameters);

                if (result == null)
                {
                    //there is no user with the email, return false
                    return false;
                }
               else if (result.userpassword == password)
                {
                    //password matches set the model to the user's data and return tru
                    _model = result;
                    return true;
                }
                else
                {
                    //password doesn't match, return false
                    return false;
                }   
            }
        }
        public UserModel GetUserSignOnInfo() { return _model; }
        //method to get the data of the signed in user
        //if a user is not signed in, the model will be null

        public void SignOutUser() { _model = null; }
        //signs out the user by setting the model to null
    }
}
