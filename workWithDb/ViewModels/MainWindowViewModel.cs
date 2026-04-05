using CommunityToolkit.Mvvm.ComponentModel;
using Npgsql;
using System.Collections.Generic;
using System.Diagnostics;
using workWithDb.Models;

namespace workWithDb.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=p511_db";

        [ObservableProperty]
        private List<User> users = new();

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

            string sql = "SELECT * FROM users";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string surname = reader.GetString(2);
                int age = reader.GetInt32(3);
                string email = reader.GetString(4);

                users.Add(new User()
                {
                    Id = id,
                    Name = name,
                    Surname = surname,
                    Age = age,
                    Email = email
                });  
            }

            connection.Close();

        }
    }
}
