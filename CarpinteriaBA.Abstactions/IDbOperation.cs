using System;
using System.Collections.Generic;
using System.Text;

namespace CarpinteriaBA.Abstactions
{
    public interface IDbOperation<T>
    {
        T Save(T entity);
        IList<T> GetAll();
        T GetById(int id);
        void Delete(int id);
    }
}
