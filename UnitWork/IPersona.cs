using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace UnitWork
{
    public interface IPersona<T> where T : class
    {
        List<T> Table();
        int Insert(T obj);
        int Update(T obj);
        int Delete(int id);
    }
}
