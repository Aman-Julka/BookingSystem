using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DAL.Interfaces
{
    public interface IDBExecuter
    {
        IDbConnection GetDbConnection(string connectionString = "BookingSystemConnString");
    }
}
