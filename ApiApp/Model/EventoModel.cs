using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApp.Model
{
    public class EventoModel
    {
        public int Cod_Evento { get; set; }
        public string DscEvento { get; set; }
        public int QtdIntegrantes { get; set; }
        public int QtdRecomendadaIntegrantes { get; set; }
        public int IndSituacaoEvento { get; set; }
        public DateTime DatInicioEvento { get; set; }
        public DateTime DatFimEvento { get; set; }
        public string DscCategoriaEvento { get; set; }

    }
}