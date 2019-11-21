using ApiApp.DataAccess;
using ApiApp.Model;
using Microsoft.Win32.SafeHandles;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Runtime.InteropServices;

namespace ApiApp.Business
{
    public class UsuarioBL : IDisposable
    {

        bool disposed = false;
        //Instancia um novo safeHandler        
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();                
            }

            disposed = true;
        } 

        private UsuarioDAO dao;

        public UsuarioBL()
        {
            dao = new UsuarioDAO();
        }
        /// <summary>Método para registrar dados de usuário                
        /// </summary>
        public void CadastrarUsuario(CadUsuarioModel usuario)
        {            
            try
            {
                dao.CadastrarUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        /// <summary>Método para registrar dados de endereço                
        /// </summary>
        public int CadastrarEndereco(CadUsuarioModel usuario)
        {
            try
            {
                return dao.CadastrarEndereco(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }         
        }

        /// <summary>Método para atualizar dados de usuário                
        /// </summary>
        public void AtualizarDados(LoginUsuarioModel usuario)
        {            
            try
            {
                dao.AtualizarDados(usuario);
            }
            catch (Exception ex)
            {
                //Error.Message = $"Erro ao atualizar dados: {ex.Message}"
                throw ex;
            }
        }


        /// <summary>Método para logar e retornar dados de usuário                
        /// </summary>
        public LoginUsuarioModel Login(LoginUsuarioModel usuario)
        {
            using (var dt = dao.Login(usuario.Email, usuario.Senha))
            {
                try
                {                    
                    if(dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            usuario.NomUsuario = dr["NOM_USUARIO"].ToString();
                            usuario.DatNascimento = dr["DAT_NASCIMENTO"].ToString();
                            usuario.Endereco.Cidade = dr["CIDADE"].ToString();
                            usuario.Endereco.Cep = dr["CEP"].ToString();
                            usuario.Endereco.Estado = dr["ESTADO"].ToString();
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    //Error.Message = $"Erro ao realizar login: {ex.Message}"
                    throw ex;
                }
                finally
                {
                    dt.Clear();
                }
                return usuario;
            }
        }

    }
}

