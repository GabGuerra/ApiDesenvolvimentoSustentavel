using ApiApp.Business;
using ApiApp.Model;
using ApiApp.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using AcceptVerbsAttribute = System.Web.Http.AcceptVerbsAttribute;

namespace ApiApp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EventoController : ApiController
    {
        // GET: Evento
        private Helpers helper;

        public EventoController()
        {
            helper = new Helpers();
        }

        [AcceptVerbs("Post")]
        public void NovoEvento([FromBody]object evento)
        {
            try
            { 
                JObject eventoJson = JObject.Parse(evento.ToString());

                var eventoModel = new CadEventoModel();
                var eventoBL = new EventoBL();

                eventoBL.CadastrarEvento(eventoModel);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("GET")]
        public string BuscaEventoDestaque()
        {
            EventoBL bl = new EventoBL();
            CadEventoModel model = bl.BuscaEventoDestaque();           
            return Newtonsoft.Json.JsonConvert.SerializeObject(model);
        }
    }  
}