module StockApi
open System
open YahooFinanceApi

type StockInfo = { Symbol: string;  Date: DateTime; Value: decimal; }

/// Gets the latest stock info for the given symbol.
let getStock(symbol: string) =
    async {
        let! securities = Yahoo.Symbols(symbol).Fields(Field.Symbol, Field.RegularMarketPrice).QueryAsync() |> Async.AwaitTask
        let stock = securities.[symbol]
        
        return
            if stock <> null
            then Some { Symbol = symbol
                        Date = DateTime.Now
                        Value = stock.RegularMarketPrice |> decimal }
            else None
    }
    
