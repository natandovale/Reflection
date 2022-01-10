using System.Net;
using System.Reflection;

namespace Bytebank.Portal.Infraestrutura
{
    public class ManipuladorRequisicaoArquivo
    {
        public void Manipular(HttpListenerResponse resposta, string path)
        {
            //responsável por retornar o Assembly em execução
            var assembly = Assembly.GetExecutingAssembly();
            //Transforma endereco da requisicao em um endereco dentro do nosso assembly(DLL/EXE)
            var nomeResouce = Utilidades.ConverterPathParaNomeAssembly(path);
            var resourceStream = assembly.GetManifestResourceStream(nomeResouce);
            if (resourceStream == null)
            {
                resposta.StatusCode = 404;
                resposta.OutputStream.Close();
            }
            else
                using (resourceStream)
                {

                    var bytesResouce = new byte[resourceStream.Length];
                    resourceStream.Read(bytesResouce, 0, (int)resourceStream.Length);

                    resposta.ContentType = Utilidades.ObterTipoDeConteudo(path);
                    resposta.StatusCode = 200;
                    resposta.ContentLength64 = resourceStream.Length;

                    resposta.OutputStream.Write(bytesResouce, 0, bytesResouce.Length);

                    resposta.OutputStream.Close();
                }
        } 
    }
}
