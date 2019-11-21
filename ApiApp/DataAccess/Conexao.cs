
using System;
using System.Configuration;
using MySql.Data.MySqlClient;

public class Conexao
{


    public MySqlConnection GetConexao()
    {
        MySqlConnection con = new MySqlConnection("server=localhost;database=desenvolvimentoSustentavel;uid=root;password=; convert zero datetime=True");
        return con;        
    }



}