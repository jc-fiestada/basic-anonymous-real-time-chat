using Microsoft.Data.Sqlite;
using MiniChat.Model;


namespace MiniChat.DBServices
{
    class MessageDb
    {
        string Filename = "Messages.db";
        public void InitializeTable()
        {
            using (var connection = new SqliteConnection($"Data Source={Filename}"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS messages (
                    user TEXT,
                    message TEXT,
                    dateCreated DATETIME DEFAULT CURRENT_TIMESTAMP
                    );";
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertMessage(string message, string user)
        {
            InitializeTable();
            using (var connection = new SqliteConnection($"Data Source={Filename}"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO messages (message, user) VALUES (@message, @user)";
                    command.Parameters.AddWithValue("@message", message);
                    command.Parameters.AddWithValue("@user", user);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<MessageRequestDto> GetMessages()
        {
            InitializeTable();

            List<MessageRequestDto> messages = new List<MessageRequestDto>();

            using (var connection = new SqliteConnection($"Data Source={Filename}"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM messages";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MessageRequestDto messageInfo = new MessageRequestDto();
                            messageInfo.User = reader["user"].ToString()!;
                            messageInfo.Message = reader["message"].ToString();
                            messageInfo.date = DateTime.Parse(reader["dateCreated"].ToString()!);

                            messages.Add(messageInfo);
                        }

                    }


                }
            }

            return messages;
        }
    }
}