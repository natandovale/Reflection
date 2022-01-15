using Bytebank.Portal.Infraestrutura.Binding;
using System;
using System.Net;
using System.Text;

namespace Bytebank.Portal.Infraestrutura
{
    public class ManipuladorRequisicaoController
    {
        private readonly ActionBinder _actionBinder = new ActionBinder(); 
        public void Manipular(HttpListenerResponse resposta, string path)
        {
            var partes = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var controllerNome = partes[0];
            var actionNome = partes[1];

            var controllerNomeCompleto = $"Bytebank.Portal.Controller.{controllerNome}Controller";
            //Identifica nossas classes e criar instâncias dinamicamente
            //Criando uma instancia atraves do nome(Assembly, classe, construtor
            var controllerWrapper = Activator.CreateInstance("Bytebank.Portal", controllerNomeCompleto, new object[0]);

            //pega a classe que queriamos
            var controller = controllerWrapper.Unwrap();

            //pegando o metodo da classe
            //var methodInfo = controller.GetType().GetMethod(actionNome);
            var methodInfo = _actionBinder.ObterMethodInfo(controller, path);

            //invocando o metodo
            var resultadoAction = (string)methodInfo.Invoke(controller, new object[0]);

            //Tranformando o arquivo para bytes.
            var bufferArquivo = Encoding.UTF8.GetBytes(resultadoAction);

            resposta.StatusCode = 200;

            //Tipo de Resposta que ira ser enviada.
            resposta.ContentType = "text/html; charset=utf-8";

            //Tamanho da resposta que sera enviada ao navegador.
            resposta.ContentLength64 = bufferArquivo.Length;

            //Stream(Fluxo) de saida 
            resposta.OutputStream.Write(bufferArquivo, 0, bufferArquivo.Length);
            resposta.OutputStream.Close();

        }
    }
}
