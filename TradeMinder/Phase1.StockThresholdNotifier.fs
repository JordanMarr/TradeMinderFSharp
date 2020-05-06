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
        | Some stock, Some thresholds -> 
            // 2) Process business rules to create an alert (or not).
            let message = 
                match stock.Value with
                | value when value > thresholds.High -> Some ({ Email = thresholds.Email; Body = sprintf "'%s' stock value of $%M exceeds the maximum value of %M." stock.Symbol stock.Value thresholds.High })
                | value when value < thresholds.Low -> Some ({ Email = thresholds.Email; Body = sprintf "'%s' stock value of $%M is less than the minimum value of %M." stock.Symbol stock.Value thresholds.Low })
                | _ -> None

            // 3) Send the message (if one exists)
            match message with 
            | Some msg -> do! Messaging.sendMessage msg
            | None -> printfn "No message was sent."

        | _ -> 
            printfn "Requires stock and threshold."
    }
