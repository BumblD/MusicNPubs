using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Muzika_ir_barai
{
    public class AppDb : IDisposable
    {
        public SqlConnection Connection { get; }

        public AppDb(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
