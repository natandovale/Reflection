using Bank.Service;
using Bank.Service.Cambio;
using System.IO;
using System.Reflection;

namespace Bytebank.Portal.Controller
{
    public class CambioController : ControllerBase
    {

        private ICambioService _cambioService;

        public CambioController()
        {
            _cambioService = new CambioTesteService();
        }

        public string MXN()
        {
            var valorFinal = _cambioService.Calcular("MXN", "BRL", 1);
            
            var textoPagina = View();

            var textoResultado = textoPagina.Replace("VALOR_EM_REAIS", valorFinal.ToString());

            return textoResultado;
        }

        public string USD()
        { 

            var valorFinal = _cambioService.Calcular("USD", "BRL", 1);

            var textoPagina = View();

            var textoResultado = textoPagina.Replace("VALOR_EM_REAIS", valorFinal.ToString());

            return textoResultado;
        }
    }
}
