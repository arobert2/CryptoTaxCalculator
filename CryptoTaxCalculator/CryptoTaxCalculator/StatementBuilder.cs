using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTaxCalculator
{
    public class StatementBuilder
    {
        public string Query { get; set; }

        private int fieldCount;

        public StatementBuilder()
        {
            Query = string.Empty;
            fieldCount = 0;
        }
        /// <summary>
        /// Append a table to the query.
        /// </summary>
        /// <param name="tableName">name of table</param>
        /// <returns>This StatementBuilder</returns>
        public StatementBuilder CreateTable(string tableName)
        {
            var sb = new StringBuilder(Query);
            sb.Append("CREATE TABLE tablename ()");
            Query = sb.ToString();
            return this;
        }
        /// <summary>
        /// Add a field to a new table
        /// </summary>
        /// <param name="name">name of field</param>
        /// <param name="type">type of field</param>
        /// <returns>This StatementBuilder</returns>
        public StatementBuilder AddTableField(string name, string type)
        {
            var sb = new StringBuilder(Query);
            if (fieldCount > 0)
                sb.Insert(Query.LastIndexOf(')'), ", ");
            sb.Insert(sb.ToString().LastIndexOf(')'), string.Format("{0} {1}", name, type));
            Query = sb.ToString();
            fieldCount++;
            return this;
        }
    }
}
