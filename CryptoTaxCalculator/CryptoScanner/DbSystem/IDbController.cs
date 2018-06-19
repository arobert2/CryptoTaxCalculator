using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CryptoScanner.DbSystem
{
    public interface IDbController<T>
    {
        void Create(T model);
        void Update(T updatedObject);
        T Read(int id);
        void Delete(string tablename, int id);
        List<T> Search(int id);
    }
}
