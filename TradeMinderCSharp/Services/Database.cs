using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TradeMinderCSharp.Entities;
using TradeMinderCSharp.Interfaces;

namespace TradeMinderCSharp.Services
{
    public class Database : Interfaces.IDatabase
    {
        private IConfig _config;

        public Database(IConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Gets data info from the database
        /// </summary>
        public async Task<NotificationThresholds> GetThresholds(string symbol, string email)
        {
            using (var conn = new SqliteConnection(_config.GetConnectionString()))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Thresholds WHERE Symbol=$Symbol AND Email=$Email";
                cmd.Parameters.AddRange(new[] { new SqliteParameter("$Symbol", symbol), new SqliteParameter("$Email", email) });
                conn.Open();
                using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    if (rdr.Read())
                    {
                        return new NotificationThresholds
                        {
                            Symbol = rdr.GetString(rdr.GetOrdinal("Symbol")),
                            High = rdr.GetDecimal(rdr.GetOrdinal("High")),
                            Low = rdr.GetDecimal(rdr.GetOrdinal("Low")),
                            Email = rdr.GetString(rdr.GetOrdinal("Email"))
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
