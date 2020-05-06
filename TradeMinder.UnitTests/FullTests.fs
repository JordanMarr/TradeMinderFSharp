module FullTests
open NUnit.Framework
open StockApi
open Database
open Messaging
open System
open FsUnit
open Phase3

[<Test>]
let ``Full test: when stock is within thresholds, should not create a message.``() =
    // Prepare return values
    let stock = { StockInfo.Symbol = "MSFT"; Date = DateTime.Now; Value = 10.0M }
    let thresholds = { NotificationThresholds.Symbol = "MSFT"; Email = "jmarr@microdesk.com"; High = 15.0M; Low = 4.0M }
    
    // Stub the StockApi
    let getStock symbol = 
        symbol |> should equal "MSFT" // Assert expected argument value
        async { return Some stock } 

    // Stub the Database
    let getThresholds symbol email =
        symbol |> should equal "MSFT"
        email |> should equal "jmarr@microdesk.com"
        async { return Some thresholds }

    // Stub Messaging
    let sendMessage (msg: Message) =
        async { return () }
        
    // Run
    StockThresholdNotifier.checkStockTemplate getStock getThresholds sendMessage "MSFT" "jmarr@microdesk.com"
    |> Async.RunSynchronously
