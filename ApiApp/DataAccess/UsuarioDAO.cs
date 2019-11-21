using ApiApp.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiApp.DataAccess
{
    public class UsuarioDAO
    {
        private DataTable dt;

        public UsuarioDAO()
        {
            dt = new DataTable();
        }
        public DataTable Login(string email, string senha)
        {
            var conexaoBD = new Conexao();
            var conn = conexaoBD.GetConexao();
            try
            {
                conn.Open();
                string strSql = @"SELECT 
	                                U.NOM_USUARIO AS NOM_USUARIO,
                                    U.DAT_NASCIMENTO,
                                    E.NOM_RUA AS RUA,
                                    E.NOM_CIDADE AS CIDADE,
                                    E.UF AS ESTADO,
                                    E.COD_CEP AS CEP
	                                FROM USUARIO U
                                INNER JOIN ENDERECO E ON U.COD_ENDERECO = E.COD_ENDERECO
                                WHERE U.EMAIL = @EMAIL
                                AND U.SENHA = @SENHA";

                MySqlCommand sql = new MySqlCommand(strSql, conn);

                sql.Parameters.AddWithValue("@EMAIL", email);
                sql.Parameters.AddWithValue("@SENHA", senha);
                dt.Load(sql.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void AtualizarDados(LoginUsuarioModel usuario)
        {
            var conexaoBD = new Conexao();
            var conn = conexaoBD.GetConexao();
            conn.Open();
            string strSql = @"UPDATE USUARIO U
                                  SET  
                                       U.ENDERECO = @DSC_ENDERECO,
                                       U.CIDADE = @CIDADE
                                       U.ESTADO = @ESTADO
                                       U.COD_ENDERECO = 
                                  WHERE U.EMAIL = @EMAIL";

            MySqlCommand sql = new MySqlCommand(strSql, conn);

            sql.Parameters.AddWithValue("@DSC_ENDERECO", usuario.Endereco);
            sql.Parameters.AddWithValue("@CIDADE", usuario.Endereco.Cidade);
            sql.Parameters.AddWithValue("@ESTADO", usuario.Endereco.Estado);
            sql.Parameters.AddWithValue("@EMAIL", usuario.Email);

            sql.ExecuteNonQuery();
        }

        public int CadastrarEndereco(CadUsuarioModel usuario)
        {
            var conexaoBD = new Conexao();
            var conn = conexaoBD.GetConexao();            
            MySqlCommand sql2 = new MySqlCommand();
            try
            {
                conn.Open();
                string strSql = @"INSERT INTO ENDERECO
                                    (NOM_RUA,COD_CEP,NOM_CIDADE,UF)
                                  VALUES
                                    (@NOM_RUA,@COD_CEP,@NOM_CIDADE,@UF);";

                MySqlCommand sql = new MySqlCommand(strSql,conn); 
                sql.Connection = conn;
                sql.Parameters.AddWithValue("@NOM_RUA", usuario.Endereco.Endereco);
                sql.Parameters.AddWithValue("@COD_CEP", usuario.Endereco.Cep);
                sql.Parameters.AddWithValue("@NOM_CIDADE", usuario.Endereco.Cidade);
                sql.Parameters.AddWithValue("@UF", usuario.Endereco.Estado);

                sql.ExecuteNonQuery();

                string strSql2 = @"SELECT LAST_INSERT_ID();";
                sql2.CommandText = strSql2;
                sql2.Connection = conn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return Convert.ToInt32(sql2.ExecuteScalar());
        }


        public void CadastrarUsuario(CadUsuarioModel usuario)
        {
            var conexaoBD = new Conexao();
            var conn = conexaoBD.GetConexao();

            try
            {
                conn.Open();
                string strSql = @"INSERT INTO USUARIO
                                 (NOM_USUARIO,DAT_NASCIMENTO,EMAIL,NUM_CELULAR,SENHA,COD_ENDERECO)
                                 VALUES
                                (@NOM_USUARIO,@DAT_NASCIMENTO,@EMAIL,@NUM_CELULAR,@SENHA,@COD_ENDERECO)";

                MySqlCommand sql = new MySqlCommand(strSql, conn);
                sql.Parameters.AddWithValue("@NOM_USUARIO", usuario.NomUsuario);
                sql.Parameters.AddWithValue("@DAT_NASCIMENTO", Convert.ToDateTime(usuario.DatNascimento));
                sql.Parameters.AddWithValue("@EMAIL", usuario.Email);
                sql.Parameters.AddWithValue("@NUM_CELULAR", usuario.NumCelular);
                sql.Parameters.AddWithValue("@SENHA", usuario.Senha);
                sql.Parameters.AddWithValue("@COD_ENDERECO", usuario.Endereco.codEndereco);
                sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                conn.Close();
            }                       
        }
    }
}
