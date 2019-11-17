using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApp.Model
{
    public class EnderecoModel
    {
        public int codEndereco { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
    }
}