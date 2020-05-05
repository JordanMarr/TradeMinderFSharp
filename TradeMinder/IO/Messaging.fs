module Messaging

/// Represents a notification alert message that will be sent to the user.
type Message = {
    Email: string
    Body: string
}

/// Sends an email.
let sendMessage (message: Message) =
    async { 
        printfn "Sending message..."
        do! Async.Sleep 1000
        printfn "To: %s\nBody: %s" message.Email message.Body
    }