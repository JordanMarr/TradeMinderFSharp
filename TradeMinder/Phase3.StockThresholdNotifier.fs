module Phase3.StockThresholdNotifier
open StockApi
open Messaging

/// This pure function creates a notification (or not) based on the stock info and thresholds.
let maybeCreateMessage (stock: StockInfo) (thresholds: Database.NotificationThresholds) = 
    match stock.Value with
    | value when value > thresholds.High -> Some ({ Email = thresholds.Email; Body = sprintf "'%s' stock value of $%M exceeds the maximum value of %M." stock.Symbol stock.Value thresholds.High })
    | value when value < thresholds.Low -> Some ({ Email = thresholds.Email; Body = sprintf "'%s' stock value of $%M is less than the minimum value of %M." stock.Symbol stock.Value thresholds.Low })
    | _ -> None
    
/// Fully testable "template" function
let checkStockTemplate getLatest getThresholds sendMessage (symbol: string) (email: string) =
    async {
        // 1) IO - Get necessary data
        let! stock = getLatest symbol
        let! thresholds = getThresholds symbol email

        match stock, thresholds with
        | Some stock, Some thresholds -> 

            // 2) Pure - Process business rules to create an alert (or not).
            let message = maybeCreateMessage stock thresholds

            // 3) IO -> Send the message (if one exists)
            match message with 
            | Some msg -> 
                printfn "Sending message..."
                do! sendMessage msg
            | None -> 
                printfn "No message was sent."

        | _ -> 
            printfn "Requires stock and threshold."
    }
    
/// This function contains the logic to run the feature.
let checkStock (symbol: string) (email: string) =
    async {
        do! checkStockTemplate 
                StockApi.getLatest 
                (Database.getThresholds (Config.getConnectionString())) 
                Messaging.sendMessage symbol email
    }
