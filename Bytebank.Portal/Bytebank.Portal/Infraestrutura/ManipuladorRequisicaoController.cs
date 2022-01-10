using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Portal.Infraestrutura
{
    public class ManipuladorRequisicaoController
    {
        public void Manipular(HttpListenerResponse resposta, string path)
        {
            var partes = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var controllerNome = partes[0];
            var actionNome = partes[1];

            var controllerNomeCompleto = $"Bytebank.Portal.Controller.{controllerNome}Controller";

            var controller = new controllerNomeCompleto();

        }
    }
}
