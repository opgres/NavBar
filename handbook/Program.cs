// See https://aka.ms/new-console-template for more information

using Npgsql;

internal class Program
{
    private static void Main(string[] args)
    {
        string connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";
        string sql = "SELECT 1";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            // Создание команды SQL
            using (var command = new NpgsqlCommand("SELECT 1", connection))
            using (var reader = command.ExecuteReader())
            {
                // Чтение данных из результата запроса
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(0)); // Вывод данных первого столбца
                }
            }
        }
    }
}