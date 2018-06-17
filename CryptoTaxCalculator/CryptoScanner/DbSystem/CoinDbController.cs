using CryptoScanner.Model;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Text;

namespace CryptoScanner.DbSystem
{
    public class CoinDbController : IDbController<DbCoinModel>
    {
        public string ConnectionString { get; private set; }

        public CoinDbController(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Create(DbCoinModel model)
        {
            using (OdbcConnection connection = new OdbcConnection(ConnectionString))
            {
                connection.Open();
                using (OdbcCommand command = new OdbcCommand("INSERT INTO " + model.Symbol + " VALUES ( " + model.TimeStamp + ", " + model.Name + ", " + model.Symbol + ", " + model.Price + " )", connection))
                    command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete(string table, int id)
        {
            using (OdbcConnection connection = new OdbcConnection(ConnectionString))
            {
                connection.Open();
                using (OdbcCommand command = new OdbcCommand("DELETE FROM " + table + " WHERE ID=" + id))
                    command.ExecuteNonQuery();
                connection.Close();
            }
        }


        public List<DbCoinModel> Search(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(DbCoinModel updatedObject)
        {
            throw new NotImplementedException();
        }

        public void CreateTable(string tablename)
        {
            using (OdbcConnection connection = new OdbcConnection(ConnectionString))
            {
                connection.Open();
                using (OdbcCommand command = new OdbcCommand("CREATE TABLE " + tablename + " ( ID int NOT NULL AUTO_INCREMENT, TimeStamp datetime NOT NULL, Name string, Symbol string NOT NULL, Price money, PRIMARY KEY(ID) )", connection))
                    command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool CheckIfTableExists(string tablename)
        {
            using (OdbcConnection connection = new OdbcConnection(ConnectionString))
            {
                connection.Open();
                using (OdbcCommand command = new OdbcCommand("SELECT CASE WHEN EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=" + tablename + " THEN TRUE ELSE FALSE END", connection))
                    return Convert.ToBoolean(command.ExecuteScalar());
            }
        }

        public DbCoinModel Read(int id)
        {
            throw new NotImplementedException();
        }
    }
}
