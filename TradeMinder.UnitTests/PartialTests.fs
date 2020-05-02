module TradeMinder.UnitTests.PartialTests
open NUnit.Framework
open StockApi
open Database
open System
open FsUnit

(*
    Here we will test only the inner business logic function, ignoring the IO calls at the beginning and end of the feature.
    I generally recommend this approach as it's the best bang for your testing buck in most cases!
*)

[<Test>]
let ``When stock is within thresholds, should not create a message.``() =
    let stock = { StockInfo.Symbol = "MSFT"; Date = DateTime.Now; Value = 10.0M }
    let thresholds = { NotificationThresholds.Symbol = "MSFT"; Email = "jmarr@microdesk.com"; High = 15.0M; Low = 4.0M }
    let result = PartiallyTestable.StockThresholdNotifier.maybeCreateMessage stock thresholds
    result |> should equal None
    
[<Test>]
let ``When stock is greater than thresholds, should create a message.``() =
    let stock = { StockInfo.Symbol = "MSFT"; Date = DateTime.Now; Value = 20.0M }
    let thresholds = { NotificationThresholds.Symbol = "MSFT"; Email = "jmarr@microdesk.com"; High = 15.0M; Low = 4.0M }
    let result = PartiallyTestable.StockThresholdNotifier.maybeCreateMessage stock thresholds
    
    match result with
    | Some msg -> Assert.IsTrue(msg.Email = "jmarr@microdesk.com")
    | None -> failwith "Expected a message."

