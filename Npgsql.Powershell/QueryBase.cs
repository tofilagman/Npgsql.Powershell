using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace Npgsql.Powershell
{ 
    public abstract class QueryBase : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string ServerInstance { get; set; } = string.Empty;

        [Parameter]
        public int Port { get; set; } = 5432;

        [Parameter(Mandatory = true)]
        public string Database { get; set; } = string.Empty;

        [Parameter(Mandatory = true)]
        public string Username { get; set; } = string.Empty;

        [Parameter(Mandatory = true)]
        public string Password { get; set; } = string.Empty;

        [Parameter(Mandatory = true)]
        public string Query { get; set; } = string.Empty;

        [Parameter]
        public int QueryTimeout { get; set; } = 0;

        protected string connectionString { get; private set; }

        protected override void BeginProcessing()
        {
            WriteVerbose("Building Connection");
            connectionString = new NpgsqlConnectionStringBuilder { Host = ServerInstance, Port = Port, Database = Database, Username = Username, Password = Password }.ConnectionString;
        } 
    }
}
