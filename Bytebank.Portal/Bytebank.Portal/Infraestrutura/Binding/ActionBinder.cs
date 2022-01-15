using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytebank.Portal.Infraestrutura.Binding
{
    //Classe que vai ligar a action(metodo) com a url
    public class ActionBinder
    {
        public object ObterMethodInfo(object controller, string path)
        {
            var idxInterrogacao = path.IndexOf('?');
            var existeQueryString = idxInterrogacao >= 0;

            if (!existeQueryString)
            {
                var nomeAction = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[1];
                var methodInfo = controller.GetType().GetMethod(nomeAction);
                return methodInfo;
            }
            else
            {
                var nomeControllerComAction = path.Substring(0, idxInterrogacao);
                var nomeAction = nomeControllerComAction.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                var queryString = path.Substring(idxInterrogacao + 1);
                var tuplasNomeValor = ObterArgumentosNomeValores(queryString);
            }
        }

        private IEnumerable<ArgumentoNomeValor> ObterArgumentosNomeValores(string queryString)
        {
            var tuplasNomeValor = queryString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var tupla in tuplasNomeValor)
            {
                var partesTupla = tupla.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                yield return new ArgumentoNomeValor(partesTupla[1], partesTupla[0]);
            }
        }
    }
}
