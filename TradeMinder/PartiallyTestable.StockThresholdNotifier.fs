module PartiallyTestable.StockThresholdNotifier
open StockApi
open Messaging

/// REFACTOR FOR TESTABILITY: Extract a pure function with all necessary data passed in as args.  We can now easily test this business logic!
let maybeCreateMessage (stock: StockInfo) (thresholds: Database.NotificationThresholds) = 
    if stock.Value > thresholds.High 
    then Some ({ Email = thresholds.Email; Body = sprintf "'%s' stock value of $%M exceeds the maximum value of %M." stock.Symbol stock.Value thresholds.High })
    elif stock.Value < thresholds.Low 
    then Some ({ Email = thresholds.Email; Body = sprintf "'%s' stock value of $%M is less than the minimum value of %M." stock.Symbol stock.Value thresholds.Low })
    else None    

/// This function contains the logic to run the feature.
let checkStock (symbol: string) (email: string) =
    async {
        // 1) IO - Get data required
        let! stock = StockApi.getLatest symbol
        let! thresholds = Database.getThresholds (Config.getConnStr()) symbol email

        match stock, thresholds with
        | Some stock, Some thresholds -> 
            // 2) Pure - Process business rules to create an alert (or not).
            let message = maybeCreateMessage stock thresholds

            // 3) IO -> Send the message (if one exists)
            match message with 
            | Some msg -> 
                printfn "Sending message..."
                do! Messaging.sendMessage msg
            | None -> 
                printfn "No message was sent."

        | _ -> 
            printfn "Requires stock and threshold."
    }
