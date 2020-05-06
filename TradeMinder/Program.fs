[<EntryPoint>]
let main argv =
    
    match argv with
    | [| symbol; email |] -> 
        Phase3.StockThresholdNotifier.checkStock symbol email
        |> Async.RunSynchronously

    | _ -> 
        printf "Invalid args.  Expected: 'TradeMinder.exe MSFT jmarr@microdesk.com'"

    0
