
using System.Web.Http.Cors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Folio_Liquidacion_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            //config.MessageHandlers.Add(new CorsPreflightHandler());

            // Rutas de API web
            var cors = new EnableCorsAttribute(
             origins: "http://localhost:5173",
             headers: "*",
             methods: "*"
         );

            config.EnableCors(cors);



            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
