[<EntryPoint>]
let main argv =
    
    match argv with
    | [| symbol; email |] -> 
        FullyTestable.StockThresholdNotifier.checkStock symbol email
        |> Async.RunSynchronously

    | _ -> 
        printf "Invalid args.  Expected: 'TradeMinder.exe MSFT jmarr@microdesk.com'"

    0
