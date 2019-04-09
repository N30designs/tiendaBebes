using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace universidadContoso.Logging
{
    public class Logger : ILogger
    {
        public void Informacion(string mensaje)
        {
            Trace.TraceInformation(mensaje);
        }

        public void Informacion(string fmt, params object[] vars)
        {
            Trace.TraceInformation(fmt, vars);
        }

        public void Informacion(Exception exception, string fmt, params object[] vars)
        {
            Trace.TraceInformation(FormatExceptionMessage(exception, fmt, vars));
        }

        public void Alerta(string mensaje)
        {
            Trace.TraceWarning(mensaje);
        }

        public void Alerta(string fmt, params object[] vars)
        {
            Trace.TraceWarning(fmt, vars);
        }

        public void Alerta(Exception exception, string fmt, params object[] vars)
        {
            Trace.TraceWarning(FormatExceptionMessage(exception, fmt, vars));
        }

        public void Error(string mensaje)
        {
            Trace.TraceError(mensaje);
        }

        public void Error(string fmt, params object[] vars)
        {
            Trace.TraceError(fmt, vars);
        }

        public void Error(Exception exception, string fmt, params object[] vars)
        {
            Trace.TraceError(FormatExceptionMessage(exception, fmt, vars));
        }

        public void TraceApi(string nombreComponente, string method, TimeSpan timespan)
        {
            TraceApi(nombreComponente, method, timespan, "");
        }

        public void TraceApi(string nombreComponente, string method, TimeSpan timespan, string fmt, params object[] vars)
        {
            TraceApi(nombreComponente, method, timespan, string.Format(fmt, vars));
        }
        public void TraceApi(string nombreComponente, string method, TimeSpan timespan, string propiedades)
        {
            string mensaje = String.Concat("Componente:", nombreComponente, ";Método:", method, ";Timespan:", timespan.ToString(), ";Propiedades:", propiedades);
            Trace.TraceInformation(mensaje);
        }

        private static string FormatExceptionMessage(Exception exception, string fmt, object[] vars)
        {
            // Simple exception formatting: for a more comprehensive version see 
            // http://code.msdn.microsoft.com/windowsazure/Fix-It-app-for-Building-cdd80df4
            var sb = new StringBuilder();
            sb.Append(string.Format(fmt, vars));
            sb.Append(" Excepción: ");
            sb.Append(exception.ToString());
            return sb.ToString();
        }
    }
}