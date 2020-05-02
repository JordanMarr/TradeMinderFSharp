module Messaging

/// Sends an email.
let sendMessage (email: string) (body: string) =
    async { 
        printfn "Sending message..."
        do! Async.Sleep 1000
        printfn "To: %s\nBody: %s" email body
    }