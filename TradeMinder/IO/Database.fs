module Database
open Microsoft.Data.Sqlite
open System.Data

/// A user defined set of stock notification thresholds.
type NotificationThresholds = {
    Symbol: string
    Low: decimal
    High: decimal
    Email: string
}

type ConnectionString = string

/// Gets data info from the database
let getThresholds (connStr: ConnectionString) (symbol: string) (email: string) =
    async {
        use conn = new SqliteConnection(connStr)
        use cmd = conn.CreateCommand()
        cmd.CommandText <- "SELECT * FROM Thresholds WHERE Symbol=$Symbol AND Email=$Email"
        cmd.Parameters.AddRange [ SqliteParameter("$Symbol", symbol); SqliteParameter("$Email", email) ]
        conn.Open()
        use! rdr = cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection) |> Async.AwaitTask

        return 
            if (rdr.Read())
            then Some { Symbol = rdr.GetString(rdr.GetOrdinal("Symbol"))
                        High = rdr.GetDecimal(rdr.GetOrdinal("High"))
                        Low = rdr.GetDecimal(rdr.GetOrdinal("Low"))
                        Email = rdr.GetString(rdr.GetOrdinal("Email")) }
            else None
    }
