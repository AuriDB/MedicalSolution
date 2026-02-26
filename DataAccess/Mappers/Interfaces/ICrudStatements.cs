using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Mappers.Interfaces
{
    public interface ICrudStatements
    {
        SqlOperation GetCreateStatement(BaseClass dto);
        SqlOperation GetUpdateStatement(BaseClass dto);
        SqlOperation GetDeleteStatement(BaseClass dto);
        SqlOperation GetRetrieveAllStatement();
        SqlOperation GetRetrieveByIdStatement(int pId);
    }
}
