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


            //responsável por retornar o Assembly em execução
            var assembly = Assembly.GetExecutingAssembly();
            //Transforma endereco da requisicao em um endereco dentro do nosso assembly(DLL/EXE)
            var nomeResouce = Utilidades.ConverterPathParaNomeAssembly(path);
            var resourceStream = assembly.GetManifestResourceStream(nomeResouce);
            var bytesResouce = new byte[resourceStream.Length];
            resourceStream.Read(bytesResouce, 0, (int)resourceStream.Length);

            resposta.ContentType = Utilidades.ObterTipoDeConteudo(path);
            resposta.StatusCode = 200;
            resposta.ContentLength64 = resourceStream.Length;

            resposta.OutputStream.Write(bytesResouce, 0, bytesResouce.Length);

            resposta.OutputStream.Close();
            httpListener.Stop();
        }
    }
}
