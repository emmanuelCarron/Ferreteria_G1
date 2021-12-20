using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using FerreteriaNetCore.DAO.comun;
using FerreteriaNetCore.Models.Entities;
using System;

namespace FerreteriaNetCore.DAO
{
    public class ProductDAO
    {
        private ISession session;

        public ProductDAO(ISession session)
        {
            this.session = session;
        }

        public IList<ProductModel> GetProducts(
            List<AtributoBusqueda> atributosBusqueda,
            string query,
            Paginado paginado,
            Ordenamiento ordenamiento,
            List<Asociacion> asociaciones,
            out long cantidadTotal)
        {
            ICriteria lista = this.session.CreateCriteria<ProductModel>("ProductModel");
            ICriteria cantidad = this.session.CreateCriteria<ProductModel>("ProductModel");

            UtilidadesNHibernate.CrearAsociaciones(asociaciones, lista);
            UtilidadesNHibernate.CrearAsociaciones(asociaciones, cantidad);

            UtilidadesNHibernate.AgregarCriteriosDeBusqueda(atributosBusqueda, query, lista);
            UtilidadesNHibernate.AgregarCriteriosDeBusqueda(atributosBusqueda, query, cantidad);
            
            UtilidadesNHibernate.AgregarOrdenamiento(ordenamiento, lista);
            UtilidadesNHibernate.AgregarPaginado(paginado, lista);
            
            cantidadTotal = UtilidadesNHibernate.ObtenerCantidad(cantidad);

            IList<ProductModel> productos = lista.List<ProductModel>();

            return productos;
        }

        public ProductModel GetProduct(string name, string brand)
        {
            ICriteria criterio = this.session.CreateCriteria<ProductModel>();

            criterio.Add(Restrictions.Eq("Name", name));
            criterio.Add(Restrictions.Eq("Brand", brand));

            IList<ProductModel> products = criterio.List<ProductModel>();

            if(products != null && products.Count > 0) return products[0];

            return null;
        }

         public IList<ProductModel> GetProduct(string name)
        {
            ICriteria criterio = this.session.CreateCriteria<ProductModel>();

            criterio.Add(Restrictions.Eq("Name", name));

            IList<ProductModel> products = criterio.List<ProductModel>();

            if(products != null && products.Count > 0) return products;

            return null;
        }

        public void SaveProduct(ProductModel product){

            this.session.Save(product);
        }
        public void SaveProduct(IProductBO newProduct){

            ProductModel prod = new ProductModel();
            prod.Name = newProduct.Name;
            prod.Brand = newProduct.Brand;
            prod.Category = newProduct.Category;
            prod.Description = newProduct.Description;
            prod.Quantity = newProduct.Quantity;
            this.session.Save(prod);
        }


        public ProductModel GetProduct(long id)
        {
            try
            {
                return this.session.Get<ProductModel>(id);
            }
            catch (Exception ex)
            {
                throw new Exception("FerreteriaNetCore.dao.ProductDao.GetProduct(long id): Error al obtener el item con id = " + id.ToString(), ex);
            }
        }

    }
}