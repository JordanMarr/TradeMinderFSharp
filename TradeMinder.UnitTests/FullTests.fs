module FullTests
open NUnit.Framework
open StockApi
open Database
open System
open FsUnit

(*
    Here we will test the function implementation, passing in stubbed versions of the IO infrastructure functions.
    I generally think this approach is overkill unless there is high cyclomatic complexity around the IO calls.
*)

[<Test>]
let ``Full test: when stock is within thresholds, should not create a message.``() =
    let stock = { StockInfo.Symbol = "MSFT"; Date = DateTime.Now; Value = 10.0M }
    let thresholds = { NotificationThresholds.Symbol = "MSFT"; Email = "jmarr@microdesk.com"; High = 15.0M; Low = 4.0M }
    
    // Stub the StockApi
    let getLatestStub symbol = 
        symbol |> should equal "MSFT"
        async {
            return Some stock
        } 

    // Stub the Database
    let getThresholdsStub symbol email =
        symbol |> should equal "MSFT"
        email |> should equal "jmarr@microdesk.com"
        Some thresholds
        
    // Run
    FullyTestable.StockThresholdNotifier.checkStockAbstract getLatestStub getThresholdsStub "MSFT" "jmarr@microdesk.com"
    |> Async.RunSynchronously
    
