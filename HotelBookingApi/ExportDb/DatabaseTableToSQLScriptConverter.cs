using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ExportDb{
    public class DatabaseTableToSQLScriptConverter {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\raczk\\Documents\\Github Clones\\bwater\\HotelBookingApi\\ExportDb\\ProductionDb.mdf\";Integrated Security=True";


        public DatabaseTableToSQLScriptConverter(string connectionString) {
            this.connectionString = connectionString;
        }

        public string CopyTableToSQLScript(string tableName) {
            StringBuilder sqlScript = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();

                // Erstellen Sie einen Befehl, um die Tabellendefinition abzurufen.
                string query = $"SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH " +
                    $"FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";

                using (SqlCommand command = new SqlCommand(query, connection)) {
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        if (reader.HasRows) {
                            // Erstellen Sie die CREATE TABLE-Anweisung.
                            sqlScript.Append($"CREATE TABLE {tableName} (\n");

                            while (reader.Read()) {
                                string columnName = reader["COLUMN_NAME"].ToString();
                                string dataType = reader["DATA_TYPE"].ToString();
                                string length = reader["CHARACTER_MAXIMUM_LENGTH"].ToString();

                                string columnDefinition = $"{columnName} {dataType}";

                                if (!string.IsNullOrEmpty(length)) {
                                    columnDefinition += $"({length})";
                                }

                                sqlScript.Append(columnDefinition);
                                sqlScript.Append(",\n");
                            }

                            // Entfernen Sie das letzte Komma und Zeilenumbruch.
                            sqlScript.Remove(sqlScript.Length - 2, 2);

                            sqlScript.Append("\n);");
                        }
                        else {
                            throw new Exception($"Tabelle '{tableName}' nicht gefunden.");
                        }
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter(this.ToString())) {
                // Schreiben Sie den SQL-String in die Datei
                writer.Write(sqlScript.ToString());
            }
            return sqlScript.ToString();

        }
        
    }

}
