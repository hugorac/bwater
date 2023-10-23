using System;


namespace ExportDb {
    internal class Program {
        static void Main(string[] args) {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\raczk\\Documents\\Github Clones\\bwater\\HotelBookingApi\\ExportDb\\ProductionDb.mdf\";Integrated Security=True";

            DatabaseTableToSQLScriptConverter agent = new DatabaseTableToSQLScriptConverter(connectionString);
            agent.CopyTableToSQLScript("Kunden");
            Console.ReadLine();
        
        }
    }
}