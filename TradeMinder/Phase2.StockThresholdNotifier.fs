module Phase2.StockThresholdNotifier
open StockApi
open Messaging

/// Extract a pure function with all necessary data passed in as args.  
let maybeCreateMessage (stock: StockInfo) (thresh: Database.NotificationThresholds) = 
    match stock.Value with
    | value when value > thresh.High -> 
        Some ({ Email = thresh.Email
                Body = sprintf "'%s' value $%M exceeds max: %M." stock.Symbol value thresh.High })
    | value when value < thresh.Low -> 
        Some ({ Email = thresh.Email
                Body = sprintf "'%s' value $%M is less than min %M." stock.Symbol value thresh.Low })
    | _ -> None 

/// This function contains the logic to run the feature.
let checkStock (symbol: string) (email: string) =
    async {
        // 1) IO - Get data required
        let! stock = StockApi.getStock symbol
        let! thresholds = Database.getThresholds (Config.getConnectionString()) symbol email

        match stock, thresholds with
        | Some stock, Some thresholds -> 
            // 2) Pure - Process business rules to create an alert (or not).
            let message = maybeCreateMessage stock thresholds

            // 3) IO -> Send the message (if one exists)
            match message with 
            | Some msg -> do! Messaging.sendMessage msg
            | None -> printfn "No message was sent."

        | _ -> 
            printfn "Requires stock and threshold."
    }
