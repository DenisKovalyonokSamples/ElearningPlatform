using DK.Api.Attributes;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DK.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //GlobalConfiguration.Configuration.Filters.Add(new NhSessionManagementAttribute());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.UseXmlSerializer = true;

            InitializeSessionFactory();
        }

        #region NHibernate

        public static ISessionFactory SessionFactory { get; set; }

        private void InitializeSessionFactory()
        {
            var nhConfig = new Configuration().Configure();
            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(DK.Dal.Mappings.ClientMap).Assembly.GetTypes());
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            nhConfig.AddMapping(domainMapping);
            SessionFactory = nhConfig.BuildSessionFactory();

#if DEBUG
            //Update database schema
            new SchemaUpdate(nhConfig).Execute(true, true);

            //Restore database schema
            //new SchemaExport(nhConfig).Execute(true, true, false);
#endif
        }

        public static ISession GetCurrentSession()
        {
            if (!CurrentSessionContext.HasBind(SessionFactory))
                CurrentSessionContext.Bind(SessionFactory.OpenSession());

            return SessionFactory.GetCurrentSession();
        }

        public static void DisposeCurrentSession()
        {
            ISession currentSession = CurrentSessionContext.Unbind(SessionFactory);

            currentSession.Close();
            currentSession.Dispose();
        }

        #endregion
    }
}
