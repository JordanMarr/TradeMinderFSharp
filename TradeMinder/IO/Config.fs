/// Represents the app.config file.
module Config

/// Gets the connection string from app.config (NOTE: refactor into Utils module)
let getConnectionString() = 
    // System.ConfigurationManager.ConnectionStrings["MyConn"]
    @"Data Source=C:\Users\jmarr\Documents\VSCode\TradeMinder\Sqlite\TradeMinder.db"
