module Database

/// A user defined set of stock notification thresholds.
type NotificationThresholds = {
    Symbol: string
    Low: decimal
    High: decimal
    Email: string
}

/// Gets data info from the database
let getThresholds (symbol: string) (email: string) =
    if symbol = "MSFT" then Some { Symbol = "MSFT"; High = 75.00M; Low = 65.00M; Email = "jmarr@microdesk.com" }
    else None
