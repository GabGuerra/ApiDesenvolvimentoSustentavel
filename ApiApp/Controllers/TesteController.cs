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
    public class TesteController : ApiController
    {

        private Helpers helper;

        public TesteController()
        {
            helper = new Helpers();

        }

        // POST: api/Teste
        [AcceptVerbs("Post")]
        public string CadastrarUsuario([FromBody]object usuario)
        {
            try
            {
                JObject usuarioJson = JObject.Parse(usuario.ToString());                
                CadUsuarioModel model = new CadUsuarioModel();
                model.NomUsuario = (string)usuarioJson.SelectToken("nome");
                model.DatNascimento = (string)usuarioJson.SelectToken("datnascimento");
                model.Email = (string)usuarioJson.SelectToken("email");
                model.NumCelular = (string)usuarioJson.SelectToken("celular");
                model.Senha = (string)usuarioJson.SelectToken("senha");
                var usuarioBL = new UsuarioBL();
                usuarioBL.CadastrarUsuario(model);
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

        // DELETE: api/Teste/5
        public void Delete(int id)
        {
        }
    }
}
