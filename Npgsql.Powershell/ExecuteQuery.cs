using System;
using System.Data;
using System.Management.Automation;

namespace Npgsql.Powershell
{
    [Cmdlet("Invoke", "Npgsqlcmd")]
    public class ExecuteQuery : QueryBase
    {

        [Parameter]
        public SwitchParameter Void { get; set; }

        [Parameter]
        public SwitchParameter Scalar { get; set; }

        protected override void ProcessRecord()
        {
            var npg = new NpgsqlConnection(connectionString);
            try
            {
                WriteVerbose("Opening Connection");
                npg.Open();
                var cmd = new NpgsqlCommand(Query, npg);

                WriteVerbose("Executing Query");
                cmd.CommandTimeout = QueryTimeout;
                cmd.CommandType = System.Data.CommandType.Text;

                if (Void.IsPresent)
                {
                    cmd.ExecuteNonQuery();
                }
                else if (Scalar.IsPresent)
                {
                    WriteObject(cmd.ExecuteScalar());
                }
                else
                {
                    using (var adp = new NpgsqlDataAdapter(cmd))
                    {
                        using(var dt = new DataTable())
                        {
                            adp.Fill(dt);
                            WriteObject(dt, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                npg?.Dispose();
            }
        }
    }
}
