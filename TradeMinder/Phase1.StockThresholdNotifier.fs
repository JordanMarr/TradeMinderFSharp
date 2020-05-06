module Phase1.StockThresholdNotifier
open StockApi
open Messaging

/// This function contains the logic to run the feature.
let checkStock (symbol: string) (email: string) =
    async {
        // 1) Get data required
        let! stock = StockApi.getStock symbol
        let! thresholds = Database.getThresholds (Config.getConnectionString()) symbol email

        match stock, thresholds with
        | Some stock, Some thresh -> 
            // 2) Process business rules to create an alert (or not).
            let message = 
                match stock.Value with
                | value when value > thresh.High -> 
                    Some ({ Email = thresh.Email
                            Body = sprintf "'%s' value $%M exceeds max: %M." stock.Symbol value thresh.High })
                | value when value < thresh.Low -> 
                    Some ({ Email = thresh.Email
                            Body = sprintf "'%s' value $%M is less than min %M." stock.Symbol value thresh.Low })
                | _ -> None 

            // 3) Send the message (if one exists)
            match message with 
            | Some msg -> do! Messaging.sendMessage msg
            | None -> printfn "No message was sent."

        | _ -> 
            printfn "Requires stock and threshold."
    }
