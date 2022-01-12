using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Bytebank.Portal.Controller
{
    public abstract class ControllerBase
    {
        protected string View([CallerMemberName]string nomeArquivo = null)
        {
            var type = GetType();
            var diretorioNome = type.Name.Replace("Controller", ""); 
            var nomeCompletoResource = $"Bytebank.Portal.View.{diretorioNome}.{nomeArquivo}.html";
            var assembly = Assembly.GetExecutingAssembly(); 
            var streamRecurso = assembly.GetManifestResourceStream(nomeCompletoResource);
            var streamLeitura = new StreamReader(streamRecurso);

            var textoPagina = streamLeitura.ReadToEnd();

            return textoPagina;
        }
    }
}
