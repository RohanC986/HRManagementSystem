using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystemCore.DataAccessLayer
{
    internal interface IDataAccessService
    {

        object ExecuteScalar(string storedprocedure, Dictionary<string, object> parameters);

        int ExecuteNonQuery(string storedProcedure, Dictionary<string, object> parameters);

        DataTable ExecuteDataSet<T>(string storedProcedure, Dictionary<string, object> parameters);

    }
}
