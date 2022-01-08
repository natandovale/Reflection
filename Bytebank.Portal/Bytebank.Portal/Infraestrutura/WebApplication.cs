using System;
using System.Net;
using System.Reflection;
using System.Text;

namespace Bytebank.Portal.Infraestrutura
{
    public class WebApplication
    {

        private readonly string[] _prefixos;

        public WebApplication(string[] prefixos)
        {
            if (prefixos == null) throw new ArgumentNullException(nameof(prefixos)); 
            _prefixos = prefixos;
        }

        public void Iniciar()
        {
            while (true)
            {
                ManipularRequisicao();
            }
        }

        private void ManipularRequisicao()
        {
            var httpListener = new HttpListener();

            foreach (var prefixo in _prefixos)
                httpListener.Prefixes.Add(prefixo);

            httpListener.Start();

            var contexto = httpListener.GetContext();
            var requisicao = contexto.Request;
            var resposta = contexto.Response;

            var path = requisicao.Url.AbsolutePath;


            //Retorna o nosso documento styles.cs ou main.js
            if (path == "/Assets/css/styles.css")
            {
                //responsável por retornar o Assembly em execução
                var assembly = Assembly.GetExecutingAssembly();
                var nomeResouce = "Bytebank.Portal.Assets.css.styles.css";
                var resourceStream = assembly.GetManifestResourceStream(nomeResouce);
                var bytesResouce = new byte[resourceStream.Length];
                resourceStream.Read(bytesResouce, 0, (int)resourceStream.Length);

                resposta.ContentType = "text/css; charset=utf-8";
                resposta.StatusCode = 200;
                resposta.ContentLength64 = resourceStream.Length;

                resposta.OutputStream.Write(bytesResouce, 0, bytesResouce.Length);

                resposta.OutputStream.Close();
            }
            else if (path == "/Assets/js/main.js")
            {
                //responsável por retornar o Assembly em execução
                var assembly = Assembly.GetExecutingAssembly();
                var nomeResouce = "Bytebank.Portal.Assets.js.main.js";
                var resourceStream = assembly.GetManifestResourceStream(nomeResouce);
                var bytesResouce = new byte[resourceStream.Length];
                resourceStream.Read(bytesResouce, 0, (int)resourceStream.Length);

                resposta.ContentType = "application/js; charset=utf-8";
                resposta.StatusCode = 200;
                resposta.ContentLength64 = resourceStream.Length;

                resposta.OutputStream.Write(bytesResouce, 0, bytesResouce.Length);

                resposta.OutputStream.Close();
            }

            httpListener.Stop();
        }
    }
}
