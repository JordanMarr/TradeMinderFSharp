module StockApi
open System

type StockInfo = {
    Symbol: string
    Date: DateTime
    Value: decimal
}

let getLatest(symbol: string) =
    async {
        // TODO: Implement stock web api
        return
            if symbol = "MSFT"
            then Some { Symbol = "MSFT"; Date = DateTime.Now; Value = 56.50M }
            else None
    }
    