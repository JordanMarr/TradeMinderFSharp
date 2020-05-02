/// This module is responsible for querying stocks via a public stocks api.
module StockApi
open System

type StockInfo = {
    Symbol: string
    Date: DateTime
    Value: decimal
}

/// Gets the latest stock info for the given symbol.
let getLatestFor(symbol: string) =
    // TODO: Implement stock web api
    { Symbol = "MSFT"; Date = DateTime.Now; Value = 56.50M }

