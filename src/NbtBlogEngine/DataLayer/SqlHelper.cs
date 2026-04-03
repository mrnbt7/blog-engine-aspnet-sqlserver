using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NbtBlogEngine.DataLayer
{
    public interface ISqlHelper
    {
        SqlDataReader ExecuteReader(string commandText, CommandType commandType = CommandType.Text, params SqlParameter[] parameters);

        int ExecuteNonQuery(string commandText, CommandType commandType = CommandType.Text, params SqlParameter[] parameters);

        object ExecuteScalar(string commandText, CommandType commandType = CommandType.Text, params SqlParameter[] parameters);
    }

    public class SqlHelper : ISqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper()
            : this(ConfigurationManager.ConnectionStrings["SqlDbConnection"]?.ConnectionString)
        {
        }

        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public SqlDataReader ExecuteReader(string commandText, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            var connection = new SqlConnection(_connectionString);
            var command = CreateCommand(connection, commandText, commandType, parameters);
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            return Execute(commandText, commandType, parameters, cmd => cmd.ExecuteNonQuery());
        }

        public object ExecuteScalar(string commandText, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            return Execute(commandText, commandType, parameters, cmd => cmd.ExecuteScalar());
        }

        private SqlCommand CreateCommand(SqlConnection connection, string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            var command = new SqlCommand(commandText, connection) { CommandType = commandType };
            if (parameters?.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }

        private T Execute<T>(string commandText, CommandType commandType, SqlParameter[] parameters, Func<SqlCommand, T> operation)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = CreateCommand(connection, commandText, commandType, parameters))
            {
                connection.Open();
                return operation(command);
            }
        }
    }
}
