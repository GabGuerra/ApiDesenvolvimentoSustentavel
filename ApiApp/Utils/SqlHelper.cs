using System;
using System.Data;
using System.Data.SqlClient;

static class SqlHelper
{
    //Método para insert/update
    public static Int32 ExecuteNonQuery(String connectionString, String commandText,
        CommandType commandType, params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }

   //Método para retornar primeira coluna da primeira linha do resultado
    public static Object ExecuteScalar(String connectionString, String commandText,
        CommandType commandType, params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }

    //Retorna data reader
    public static SqlDataReader ExecuteReader(String connectionString, String commandText,
        CommandType commandType, params SqlParameter[] parameters)
    {
        SqlConnection conn = new SqlConnection(connectionString);

        using (SqlCommand cmd = new SqlCommand(commandText, conn))
        {
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(parameters);

            conn.Open();            
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }
    }
}