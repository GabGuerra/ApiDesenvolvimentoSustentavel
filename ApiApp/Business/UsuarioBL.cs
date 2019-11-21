using ApiApp.Model;
using MySql.Data.MySqlClient;
using System;


namespace ApiApp.Business
{
    public class UsuarioBL
    {        

        public void CadastrarUsuario(CadUsuarioModel usuario)
        {
            var conexaoBD = new Conexao();
            var conn = conexaoBD.GetConexao();
            try
            {
                conn.Open();
                string strSql = @"INSERT INTO
                                    USUARIO
                             (NOM_USUARIO,DAT_NASCIMENTO,EMAIL,NUM_CELULAR,SENHA)
                              VALUES
                             (@NOM_USUARIO,@DAT_NASCIMENTO,@EMAIL,@NUM_CELULAR,@SENHA)";

                MySqlCommand sql = new MySqlCommand(strSql, conn);                
                sql.Parameters.AddWithValue("@NOM_USUARIO", usuario.NomUsuario);
                sql.Parameters.AddWithValue("@DAT_NASCIMENTO", Convert.ToDateTime(usuario.DatNascimento));
                sql.Parameters.AddWithValue("@EMAIL", usuario.Email);
                sql.Parameters.AddWithValue("@NUM_CELULAR", usuario.NumCelular);
                sql.Parameters.AddWithValue("@SENHA", usuario.Senha);
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

        public LoginUsuarioModel Login(LoginUsuarioModel usuario)
        {
            var conexaoBD = new Conexao();
            var conn = conexaoBD.GetConexao();
            try
            {
                conn.Open();
                string strSql = @"SELECT 
                                    NOM_USUARIO,
                                    DAT_NASCIMENTO,
                                    EMAIL,
                                    NUM_CELULAR
                                  FROM USUARIO 
                                  WHERE EMAIL = @EMAIL
                                  AND SENHA = @SENHA;";

                MySqlCommand sql = new MySqlCommand(strSql, conn);

                sql.Parameters.AddWithValue("@EMAIL", usuario.Email);      
                sql.Parameters.AddWithValue("@SENHA", usuario.Senha);
                var dr = sql.ExecuteReader();

                LoginUsuarioModel model = new LoginUsuarioModel();
                while (dr.Read())
                {
                    model.NomUsuario = dr["NOM_USUARIO"].ToString();
                    model.DatNascimento = dr["DAT_NASCIMENTO"].ToString();
                    model.Email = dr["EMAIL"].ToString();
                    model.NumCelular = dr["NUM_CELULAR"].ToString();
                }

                return model;
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

