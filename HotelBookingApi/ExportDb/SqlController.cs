using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportDb {
    public class SqlController {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\raczk\\Documents\\Github Clones\\bwater\\HotelBookingApi\\ExportDb\\ProductionDb.mdf\";Integrated Security=True";
        
        public string GetDbScheme() {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT table_name, column_name FROM information_schema.columns";
            using (SqlCommand command = new SqlCommand(query, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    string tableName = string.Empty;
                    string columnName = string.Empty;
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
