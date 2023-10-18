using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyDbSchema {
    public class SqlController {

        public string GetDbScheme() {
            string query = "SELECT table_name, column_name FROM information_schema.columns";
            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    string tableName;
                    string columnName;
                    while (reader.Read()) {
                        tableName = reader["table_name"].ToString();
                        columnName = reader["column_name"].ToString();
                        // Hier kannst du die Tabellen- und Spalteninformationen verwenden oder speichern
                    }
                    return tableName + columnName;
                }
            }
        }
    }
}
