using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace universidadContoso.Logging
{
    public interface ILogger
    {
        void Informacion(string message);
        void Informacion(string fmt, params object[] vars);
        void Informacion(Exception exception, string fmt, params object[] vars);

        void Alerta(string message);
        void Alerta(string fmt, params object[] vars);
        void Alerta(Exception exception, string fmt, params object[] vars);

        void Error(string message);
        void Error(string fmt, params object[] vars);
        void Error(Exception exception, string fmt, params object[] vars);

        void TraceApi(string componentName, string method, TimeSpan timespan);
        void TraceApi(string componentName, string method, TimeSpan timespan, string properties);
        void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars);

    }
}