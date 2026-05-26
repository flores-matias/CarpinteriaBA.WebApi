using System;
using System.Collections.Generic;
using System.Text;

namespace CarpinteriaBA.Abstactions
{
    public interface IDbContext<T>: IDbOperation<T> where T : class
    {

    }
}
