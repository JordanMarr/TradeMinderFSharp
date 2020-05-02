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
        return Some { Symbol = "MSFT"; Date = DateTime.Now; Value = 56.50M }
    }
    
