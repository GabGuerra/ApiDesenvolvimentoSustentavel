using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApp.Model
{
    public class CadUsuarioModel
    {        
        public string NomUsuario { get; set; }
        public string DatNascimento { get; set; }
        public string Senha { get; set; }       
        
        public EnderecoModel Endereco { get; set; }
        public string NumCelular { get; set; }
        public string Email { get; set; }

        public CadUsuarioModel()
        {
            Endereco = new EnderecoModel();
        }
    }
}