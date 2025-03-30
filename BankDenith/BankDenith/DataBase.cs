using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BankDenith
{
    class DataBase
    {
        public MySqlConnection connectDB()
        {
            string connection_string = "Server = localhost;Database = bank;UserID =root;Password =Nadun2007 ;";
            MySqlConnection connection = new MySqlConnection(connection_string);
            return connection;
        }
    }
}
