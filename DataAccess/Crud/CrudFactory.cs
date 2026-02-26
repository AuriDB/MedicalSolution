using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Crud
{
    public abstract class CrudFactory
    {
        protected SqlDao _sqlDao;

        public abstract void Create(BaseClass dto);
        public abstract void Update(BaseClass dto);
        public abstract void Delete(BaseClass dto);
        public abstract List<T> RetrieveAll<T>(); //T es un valor generico, es decir, puede ser cualquier tipo de dato
        public abstract List<T> RetrieveById<T>(int pId);
    }
}
