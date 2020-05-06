module StockApi
open System

type StockInfo = { Symbol: string;  Date: DateTime; Value: decimal; }

/// Gets the latest stock info for the given symbol.
let getStock(symbol: string) =
    async {
        // TODO: Implement stock web api
        return
            if symbol = "MSFT"
            then Some { Symbol = "MSFT"; Date = DateTime.Now; Value = 56.50M }
            else None
    }
    
