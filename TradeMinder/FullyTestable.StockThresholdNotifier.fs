module FullyTestable.StockThresholdNotifier

open StockApi

/// Represents a notification alert message that will be sent to the user.
type Message = {
    Email: string
    Body: string
}

/// Gets the connection string from app.config (NOTE: refactor into Utils module)
let getConnStr() = 
    // TODO: get from ConfigurationManager / app.config
    "my connection string..."


/// This pure function creates a notification (or not) based on the stock info and thresholds.
let maybeCreateMessage (stock: StockInfo) (thresholds: Database.NotificationThresholds) = 
    if stock.Value > thresholds.High 
    then Some ({ Email = thresholds.Email; Body = sprintf "'%s' stock value of $%M exceeds the maximum value of %M." stock.Symbol stock.Value thresholds.High })
    elif stock.Value < thresholds.Low 
    then Some ({ Email = thresholds.Email; Body = sprintf "'%s' stock value of $%M is less than the minimum value of %M." stock.Symbol stock.Value thresholds.Low })
    else None   
    
/// Testable function
let checkStockAbstract getLatest getThresholds (symbol: string) (email: string) =
    async {
        // 1) IO - Get necessary data
        let! stock = getLatest symbol
        let thresholds = getThresholds symbol email

        match stock, thresholds with
        | Some stock, Some thresholds -> 

            // 2) Pure - Process business rules to create an alert (or not).
            let message = maybeCreateMessage stock thresholds

            // 3) IO -> Send the message (if one exists)
            match message with 
            | Some msg -> 
                printfn "Sending message..."
                do! Messaging.sendMessage msg.Email msg.Body
            | None -> 
                printfn "No message was sent."

        | _ -> 
            printfn "Requires stock and threshold."
    }


/// This function contains the logic to run the feature.
let checkStock (symbol: string) (email: string) =
    async {
        let dbGetThresholds = Database.getThresholds (getConnStr()) // partially apply
        do! checkStockAbstract StockApi.getLatest dbGetThresholds symbol email
    }
