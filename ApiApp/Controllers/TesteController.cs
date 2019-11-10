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
                var HashMapUsuario = helper.TransformaObjetoJson(usuario.ToString());

                CadUsuarioModel model = new CadUsuarioModel();
                model.NomUsuario = HashMapUsuario["nome"];
                model.DatNascimento = HashMapUsuario["datnascimento"];
                model.Email = HashMapUsuario["email"];
                model.NumCelular = HashMapUsuario["celular"];
                model.Senha = HashMapUsuario["senha"];
                var usuarioBL = new UsuarioBL();

                usuarioBL.CadastrarUsuario(model);

                
                return JsonConvert.SerializeObject("OK");
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
                var HashMapLogin = helper.TransformaObjetoJson(usuario.ToString());

                var usuarioBL = new UsuarioBL();
                LoginUsuarioModel usuarioLogin = new LoginUsuarioModel();
                usuarioLogin.Email = HashMapLogin["email"];
                usuarioLogin.Senha = HashMapLogin["senha"];
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
