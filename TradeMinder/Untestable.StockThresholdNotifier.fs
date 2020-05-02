﻿module Untestable.StockThresholdNotifier

open StockApi

/// Represents a notification alert message that will be sent to the user.
type Message = {
    Email: string
    Body: string
}


/// This function contains the logic to run the feature.
let checkStock (symbol: string) (email: string) =
    async {
        // 1) Get data required
        let! stock = StockApi.getLatest symbol
        let thresholds = Database.getThresholds symbol email

        match stock, thresholds with
        | Some stock, Some thresholds -> 

            // 2) Process business rules to create an alert (or not).
            let message = 
                if stock.Value > thresholds.High 
                then Some ({ Email = thresholds.Email; Body = sprintf "'%s' stock value of $%M exceeds the maximum value of %M." stock.Symbol stock.Value thresholds.High })
                elif stock.Value < thresholds.Low 
                then Some ({ Email = thresholds.Email; Body = sprintf "'%s' stock value of $%M is less than the minimum value of %M." stock.Symbol stock.Value thresholds.Low })
                else None

            // 3) Send the message (if one exists)
            match message with 
            | Some msg -> 
                printfn "Sending message..."
                do! Messaging.sendMessage msg.Email msg.Body
            | None -> 
                printfn "No message was sent."

        | _ -> 
            printfn "Requires stock and threshold."
    }
