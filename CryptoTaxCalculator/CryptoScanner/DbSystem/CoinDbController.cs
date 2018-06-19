using CryptoScanner.Model;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Text;

namespace CryptoScanner.DbSystem
{
    public class CoinDbController : IDbController<DbCoinModel>
    {
        private OdbcConnection _connection { get; set; }

        public CoinDbController(string connectionString)
        {
            _connection = new OdbcConnection(connectionString);
        }

        ~CoinDbController()
        {
            _connection.Dispose();
        }

        public void Create(DbCoinModel model)
        {
                _connection.Open();
                using (OdbcCommand command = new OdbcCommand("INSERT INTO " + model.Symbol + " VALUES ( " + model.TimeStamp + ", " + model.Name + ", " + model.Symbol + ", " + model.Price + " )", _connection))
                    command.ExecuteNonQuery();
                _connection.Close();
            
        }

        public void Delete(string table, int id)
        {

                _connection.Open();
                using (OdbcCommand command = new OdbcCommand("DELETE FROM " + table + " WHERE ID=" + id))
                    command.ExecuteNonQuery();
                _connection.Close();
            
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

                _connection.Open();
                using (OdbcCommand command = new OdbcCommand("CREATE TABLE " + tablename + " ( ID int NOT NULL AUTO_INCREMENT, TimeStamp datetime NOT NULL, Name string, Symbol string NOT NULL, Price money, PRIMARY KEY(ID) )", _connection))
                    command.ExecuteNonQuery();
                _connection.Close();
            
        }

        public bool CheckIfTableExists(string tablename)
        {
                _connection.Open();
            bool result;
            using (OdbcCommand command = new OdbcCommand("SELECT CASE WHEN EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=" + tablename + " THEN TRUE ELSE FALSE END", _connection))
                result = Convert.ToBoolean(command.ExecuteScalar());
            _connection.Close();
            return result;
            
        }

        public DbCoinModel Read(int id)
        {
            throw new NotImplementedException();
        }
    }
}
