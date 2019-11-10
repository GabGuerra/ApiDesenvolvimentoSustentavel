using java.net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApp.Utils
{
    public class Helpers
    {
        public Dictionary<string, string> TransformaObjetoJson(string objetoJson)
        {
            var usuarioEncodado = URLDecoder.decode(objetoJson, "UTF-8");
            var myHashMap = new Dictionary<string, string>();
            var parametros = usuarioEncodado.ToString().Split('&');
            foreach (var parametro in parametros)
            {

                var chave = parametro.Split('=')[0];
                var valor = parametro.Split('=')[1];
                myHashMap.Add(chave, valor);
            }

            return myHashMap;
        }
    }
}