﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Texsperts
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "GetAPI",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "Registration", action = "GetAllEmployee", id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "AddAPI",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "Registration", action = "AddEmployee", id = RouteParameter.Optional }
            );
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(config.Formatters.JsonFormatter);
        }
    }
}
