using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataGeneratorApi{
    public class DatabaseTableToSQLScriptConverter {
        private string connectionString;
        public DatabaseTableToSQLScriptConverter(string connectionString) {
            this.connectionString = connectionString;
        }

        public string CopyTableSchemeToSQLScript(string tableName) {
            StringBuilder sqlScript = new StringBuilder();
            StringBuilder insertScript = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();

                //Tabellendefinition abzurufen
                string query = $"SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH " +
                    $"FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";

                using (SqlCommand command = new SqlCommand(query, connection)) {
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        if (reader.HasRows) {
                            //CREATE TABLE-Anweisung
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

                            // Entfernt das letzte Komma und Zeilenumbruch.
                            sqlScript.Remove(sqlScript.Length - 2, 2);

                            sqlScript.Append("\n);");
                        }
                        else {
                            throw new Exception($"Tabelle '{tableName}' nicht gefunden.");
                        }
                    }
                }
                string selectQuery = $"SELECT * FROM {tableName}";
                SqlCommand cmd = new SqlCommand(selectQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);



                foreach (DataRow row in dataTable.Rows) {
                    insertScript.Append($"INSERT INTO {tableName} (");

                    foreach (DataColumn column in dataTable.Columns) {
                        string columnName = column.ColumnName;
                        insertScript.Append($"{columnName}, ");
                    }

                    insertScript.Length -= 2; // Remove the last comma and space
                    insertScript.AppendLine(") VALUES (");

                    foreach (DataColumn column in dataTable.Columns) {
                        object value = row[column];
                        string valueString = (value != DBNull.Value) ? value.ToString() : "NULL";
                        insertScript.Append($"'{valueString}', ");
                    }

                    insertScript.Length -= 2; // Remove the last comma and space
                    insertScript.AppendLine(");");
                }

            }
            return $"{sqlScript.ToString()}\n{insertScript.ToString()}";

        }
    }

}
