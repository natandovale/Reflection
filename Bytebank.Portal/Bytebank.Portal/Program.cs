using Bytebank.Portal.Infraestrutura;
using System;

namespace Bytebank.Portal
{
    class Program
    {
        static void Main(string[] args)
        {
            var prefixos = new string[] { "http://localhost:5341/" };
            var webAplication = new WebApplication(prefixos);
            webAplication.Iniciar();
        }
    }
}
