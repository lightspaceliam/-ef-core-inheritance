using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace Entity.Services.Tests.Helpers
{
    public class SqliteCommandInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            if (command.CommandText.Contains("nvarchar(max)"))
            {
                command.CommandText = command.CommandText.Replace("nvarchar(max)", "nvarchar(8000)");
            }
            return base.NonQueryExecuting(command, eventData, result);
        }
    }
}
