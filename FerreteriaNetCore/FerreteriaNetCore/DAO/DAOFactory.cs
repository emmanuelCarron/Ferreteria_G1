using System;
using NHibernate;

namespace FerreteriaNetCore.DAO
{
    public class DAOFactory : IDisposable
    {
        #region atributos privados
        private ISession session = null;
        private ITransaction transaction = null;
        #endregion
        
        #region Constructor
        public DAOFactory()
        {
            this.session = Database.Instance.SessionFactory.OpenSession();
        }
        #endregion

        #region métodos de la base de datos
        public bool BeginTrans()
        {
            try
            {
                this.transaction = this.session.BeginTransaction();
                return true;
            }
            catch(System.Exception e)
            {
                throw new System.Exception(
                    "ejemplo.dao.NHibernate.NHibernateDAOFactory.BeginTrans()", 
                    e);
            }
        }

        public bool Commit()
        {
            try
            {
                if(this.transaction == null || !this.transaction.IsActive) return false;

                this.transaction.Commit();

                this.transaction = null;

                return true;
            }
            catch(System.Exception e)
            {
                throw new System.Exception(
                    "ejemplo.dao.NHibernate.NHibernateDAOFactory.Commit()", 
                    e);
            }
        }

        public void Rollback()
        {
            try
            {
                if(this.transaction == null || !this.transaction.IsActive) return;

                this.transaction.Rollback();

                this.transaction = null;
            }
            catch(System.Exception e)
            {
                throw new System.Exception("ejemplo.dao.NHibernate.NHibernateDAOFactory.Rollback()", e);
            }
        }

        public void Close()
        {
            try
            {
                if(this.transaction != null && this.transaction.IsActive)
                {
                    this.transaction.Rollback();
                }

                this.session.Close();
            }
            catch(System.Exception e)
            {
                throw new System.Exception("ejemplo.dao.NHibernate.NHibernateDAOFactory.Close()", e);
            }
        }
        
        public void Dispose()
        {
            try
            {
                if(this.transaction != null && this.transaction.IsActive)
                {
                    this.transaction.Rollback();
                }

                this.session.Close();
            }
            catch(System.Exception e)
            {
                throw new System.Exception("ejemplo.dao.NHibernate.NHibernateDAOFactory.Dispose()", e);
            }
        }
        #endregion

        #region DAOs: Agregar los DAOs de ustedes
        private UserDAO userDAO = null;

        public UserDAO UserDAO 
        { 
            get 
            {
                if(this.userDAO == null)
                {
                    this.userDAO = new UserDAO(this.session);
                }

                return userDAO;
            }
        }

        public ProductDAO ProductDAO 
        { 
            get 
            {
                return new ProductDAO(this.session);
            
            }
        }
        #endregion
    }
}