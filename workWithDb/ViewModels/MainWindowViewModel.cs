using Npgsql;
using System.Diagnostics;

namespace workWithDb.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=store_db";

        public MainWindowViewModel()
        {
            using var connection = new NpgsqlConnection(connectionString);
            try
            {
                connection.Open();
                Debug.WriteLine("Connection opened successfully.");
            }
            catch (NpgsqlException e)
            {
                Debug.WriteLine(e.Message);
            }


            string sql = "SELECT id, name, price FROM products";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                decimal price = reader.GetDecimal(2);

                Debug.WriteLine($"{id}. {name} - {price}$");
            }

            connection.Close();

        }
    }
}
