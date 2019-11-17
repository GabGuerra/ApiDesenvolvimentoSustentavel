using ApiApp.Business;
using ApiApp.Model;
using ApiApp.Utils;
using java.net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace ApiApp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuarioController : ApiController
    {

        private Helpers helper;

        public UsuarioController()
        {
            helper = new Helpers();

        }

        /// <summary>Método para registrar dados de usuário                
        /// </summary>
        [AcceptVerbs("Post")]
        public string CadastrarUsuario([FromBody]object usuario)
        {
            try
            {
                //Recebe JSON e instancia a model
                JObject usuarioJson = JObject.Parse(usuario.ToString());                
                CadUsuarioModel model = new CadUsuarioModel();

                model.NomUsuario = (string)usuarioJson.SelectToken("nome");
                model.DatNascimento = (string)usuarioJson.SelectToken("datnascimento");
                model.Email = (string)usuarioJson.SelectToken("email");
                model.NumCelular = (string)usuarioJson.SelectToken("celular");
                model.Senha = (string)usuarioJson.SelectToken("senha");
                model.Endereco.Endereco = (string)usuarioJson.SelectToken("endereco");
                model.Endereco.Cidade = (string)usuarioJson.SelectToken("cidade");
                model.Endereco.Estado = (string)usuarioJson.SelectToken("estado");
                model.Endereco.Cep = (string)usuarioJson.SelectToken("Cep");                 
                
                
                //Instancia a BL e chama os métodos de cadastro
                var usuarioBL = new UsuarioBL();

                //Insere novo endereço e resgata código do mesmo.   
                model.Endereco.codEndereco = usuarioBL.CadastrarEndereco(model);

                //Cadastra o usuario
                usuarioBL.CadastrarUsuario(model);

                
                 //Cria objeto dinânimco para retornar email e senha via JSON                
                 dynamic ret = new System.Dynamic.ExpandoObject();
                ret.email = model.Email;
                ret.senha = model.Senha;
                return JsonConvert.SerializeObject(ret);
            }
            catch (Exception ex) 
            {
                throw ex;
            }            
        }

        /// <summary>Método para atualizar dados de usuário                
        /// </summary>
        [AcceptVerbs("Post")]
        public void AtualizarDados([FromBody]object usuario)
        {
            try
            {
                //Obtém JSON e instancia model
                JObject usuarioJson = JObject.Parse(usuario.ToString());
                LoginUsuarioModel model = new LoginUsuarioModel();

                //Preenche objeto
                model.NomUsuario = (string)usuarioJson.SelectToken("nome");
                model.DatNascimento = (string)usuarioJson.SelectToken("datnascimento");
                model.Email = (string)usuarioJson.SelectToken("email");
                model.NumCelular = (string)usuarioJson.SelectToken("celular");                
                model.Endereco.Endereco = (string)usuarioJson.SelectToken("endereco");
                model.Endereco.Cidade = (string)usuarioJson.SelectToken("endereco");
                model.Endereco.Estado =  (string)usuarioJson.SelectToken("estado");

                //Instancia BL e chama método de atualização               
                var usuarioBL = new UsuarioBL();
                usuarioBL.AtualizarDados(model);                
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>Método para logar usuário           
        /// </summary>
        [AcceptVerbs("Post")]
        public string Login([FromBody]object usuario)
        {
            try
            {               
                //Recebe JSON e transforma.
                JObject usuarioJson = JObject.Parse(usuario.ToString());

                //Instancia BL e Model, atribuindo valor à model
                var usuarioBL = new UsuarioBL();
                LoginUsuarioModel usuarioLogin = new LoginUsuarioModel();
                usuarioLogin.Email = (string)usuarioJson.SelectToken("email");
                usuarioLogin.Senha = (string)usuarioJson.SelectToken("senha");

                //Retorna a Model preenchida com valores do BD.
                return Newtonsoft.Json.JsonConvert.SerializeObject(usuarioBL.Login(usuarioLogin));
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }          
    }
}
