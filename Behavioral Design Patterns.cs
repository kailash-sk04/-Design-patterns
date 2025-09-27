interface IPaymentStrategy {
    void Pay(int amount);
}

class CreditCardPayment : IPaymentStrategy {
    public void Pay(int amount) {
        Console.WriteLine($"Paid ₹{amount} using Credit Card.");
    }
}

class PayPalPayment : IPaymentStrategy {
    public void Pay(int amount) {
        Console.WriteLine($"Paid ₹{amount} using PayPal.");
    }
}

class ShoppingCart {
    private IPaymentStrategy strategy;
    public ShoppingCart(IPaymentStrategy strategy) {
        this.strategy = strategy;
    }
    public void Checkout(int amount) {
        strategy.Pay(amount);
    }
}

class Program {
    static void Main(string[] args) {
        var cart = new ShoppingCart(new PayPalPayment());
        cart.Checkout(500);
    }
}
