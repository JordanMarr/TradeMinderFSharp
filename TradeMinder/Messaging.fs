module Messaging

type Email = string
type Body = string

/// Sends an email.
let sendMessage (email: Email) (body: Body) =
    async { 
        printfn "Sending message..."
        do! Async.Sleep 1000
        printfn "To: %s\nBody: %s" email body
    }