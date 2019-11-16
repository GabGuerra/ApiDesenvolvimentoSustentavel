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
        public string NovoEvento([FromBody]object evento)
        {
            try
            {
                //Recebe JSON e transforma.
                JObject eventoJson = JObject.Parse(evento.ToString());

                //prenche dados da model Evento
                var eventoModel = new EventoModel();

                //Instancia BL e Model, atribuindo valor à model
                var eventoBL = new EventoBL();

                //Salva no bd

                eventoBL.CadastrarEvento(eventoModel);

                //Retorna a Model preenchida com valores do BD.
                //return Newtonsoft.Json.JsonConvert.SerializeObject(eventoBL.CadastrarEvento(eventoModel));

                //return Newtonsoft.Json.JsonConvert.SerializeObject(usuarioBL.Login(usuarioLogin));
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("GET")]
        public string BuscaEventoDestaque()
        {
            EventoBL Eb = new EventoBL();
            EventoModel em= Eb.BuscaEventoDestaque();
           

            return Newtonsoft.Json.JsonConvert.SerializeObject(em);
        }
    }  
}