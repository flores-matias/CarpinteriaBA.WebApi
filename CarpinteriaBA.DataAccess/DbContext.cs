using CarpinteriaBA.Abstactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarpinteriaBA.DataAccess
{
    public class DbContext<T>:IDbContext<T> where T : class,IEntidad
    {
        DbSet<T> _Items;
        DbDataAccess _ctx;
        public DbContext(DbDataAccess ctx)
        {
            _ctx = ctx;
            _Items=_ctx.Set<T>();//ACA LO QUE HAGO ES OBTENER EL DBSET DE LA ENTIDAD T, ESTO ES PARA PODER REALIZAR LAS OPERACIONES DE 
                                //CRUD SOBRE LA ENTIDAD T, YA QUE EL DBSET ES LA REPRESENTACION DE LA TABLA EN LA BASE DE DATOS,
                                //Y CON EL DBSET PUEDO REALIZAR LAS OPERACIONES DE CRUD SOBRE LA ENTIDAD T.
        }

        public void Delete(int id)
        {
            var entity=_Items.FirstOrDefault(i=>i.Id==id);
            if(entity!=null) {_Items.Remove(entity); }
            _ctx.SaveChanges();
        }

        public IList<T> GetAll()
        {
            return _Items.ToList();
        }

        public T GetById(int id)
        {
            return _Items.FirstOrDefault(i => i.Id == id);
        }

        public T Save(T entity)
        {
            if(entity.Id.Equals(0))
            {
                _Items.Add(entity);
            }
            else
            {
                var entityDb=GetById(entity.Id);
                _ctx.Entry(entityDb).State = EntityState.Modified;//aca le digo a EF que la entidad que estoy pasando es una entidad modificada, para
                                                                  //que EF sepa que tiene que actualizarla en la base de datos, y no agregarla como una
                                                                  //nueva entidad.
                _Items.Update(entity);
            }
            _ctx.SaveChanges();
            return entity;
        }
    }
}
