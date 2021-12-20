using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace FerreteriaNetCore.DAO
{
    public class Database
    {
        //Modificar la cadena de conexión
        private static string connectionString = 
            "Server=localhost;" +
            "Database=ferreteria;" +
            "Uid=root;" +
            "Pwd=password;";

        #region Métodos del singleton
        private static Database instance = null;

        public static Database Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Database();
                }

                return instance;
            }
        }

        private Database()
        {
            //Genero mi fábrica de sesiones
            this.sessionFactory = this.CreateSessionFactory();
        }
        #endregion

        #region SessionFactory, único método público
        private ISessionFactory sessionFactory = null;

        public ISessionFactory SessionFactory 
        { 
            get 
            { 
                return this.sessionFactory; 
            }
        }
        #endregion
    
        #region Método que deben cambiar si no usan MySQL
        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard.ConnectionString(connectionString)
                )
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<Database>())
                .BuildSessionFactory();
        }
        #endregion
    }
}