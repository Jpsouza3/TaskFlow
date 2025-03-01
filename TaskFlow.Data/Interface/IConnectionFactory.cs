using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Data.Interface
{
    public interface IConnectionFactory
    {
        SQLiteConnection CreateConnection();
    }
}
