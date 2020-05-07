module ShoppingCart

// F# Unit of Measure
[<Measure>] type USD

type CouponDiscount = 
    | Discount of decimal<USD>
    | FreeOrder
    | NoDiscount

type Order = {
    Id: System.Guid
    Items: CartItem list
    Total: decimal<USD>
}

and CartItem = {
    SKU: string
    Qty: int
    Price: decimal<USD>
}

/// Calculates the total (pure / testable function)
let calculateTotal items discount =
    let subTotal = items |> List.sumBy (fun item -> item.Price)
    // Total:
    match discount with
    | Discount amt -> subTotal - (abs amt)
    | FreeOrder -> 0.00M<USD>
    | NoDiscount -> subTotal
    

/// A testable checkout template - domain logic written before IO implementations
let checkoutTemplate lookupCouponCode saveOrder (items: CartItem list) (couponCode: string) =
    // 1) IO - Get info
    let discount = lookupCouponCode couponCode 
    
    // 2) Pure logic
    let total = calculateTotal items discount

    // 3) IO - save
    saveOrder { Id = System.Guid.NewGuid(); Items = items; Total = total }
