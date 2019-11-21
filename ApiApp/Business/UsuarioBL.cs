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
        public int CadastrarEndereco(CadUsuarioModel usuario)
        {
            var conexaoBD = new Conexao();
            var conn = conexaoBD.GetConexao();
            try
            {
                conn.Open();
                string strSql = @"INSERT INTO ENDERECO
                                    (NOM_RUA,COD_CEP,NOM_CIDADE,UF)
                                  VALUES
                                    (@NOM_RUA,@COD_CEP,@NOM_CIDADE,@UF);";

                MySqlCommand sql = new MySqlCommand(strSql, conn);
                sql.Parameters.AddWithValue("@NOM_RUA", usuario.Endereco.Endereco);
                sql.Parameters.AddWithValue("@COD_CEP", usuario.Endereco.Cep);
                sql.Parameters.AddWithValue("@NOM_CIDADE", usuario.Endereco.Cidade);
                sql.Parameters.AddWithValue("@UF", usuario.Endereco.Estado);
                
                sql.ExecuteNonQuery();

                string strSql2 = @"SELECT LAST_INSERT_ID();";
                MySqlCommand sql2 = new MySqlCommand(strSql2, conn);

                return Convert.ToInt32(sql2.ExecuteScalar());
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
            try
            {
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
            catch (Exception ex)
            {
                throw ex;
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
	                                U.NOM_USUARIO AS NOM_USUARIO,
                                    U.DAT_NASCIMENTO,
                                    E.NOM_RUA AS RUA,
                                    E.NOM_CIDADE AS CIDADE,
                                    E.UF AS ESTADO,
                                    E.COD_CEP AS CEP
	                                FROM USUARIO U
                                INNER JOIN ENDERECO E ON U.COD_ENDERECO = E.COD_ENDERECO
                                WHERE U.EMAIL = @EMAIL";                                

                MySqlCommand sql = new MySqlCommand(strSql, conn);

                sql.Parameters.AddWithValue("@EMAIL", usuario.Email);                      
                var dr = sql.ExecuteReader();

                LoginUsuarioModel model = new LoginUsuarioModel();
                while (dr.Read())
                {
                    model.NomUsuario = dr["NOM_USUARIO"].ToString();
                    model.DatNascimento = dr["DAT_NASCIMENTO"].ToString();
                    model.Endereco.Endereco = dr["RUA"].ToString();
                    model.Endereco.Cidade = dr["CIDADE"].ToString();
                    model.Endereco.Estado = dr["ESTADO"].ToString();
                    model.Email = usuario.Email;
                    model.Senha = usuario.Senha;
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

