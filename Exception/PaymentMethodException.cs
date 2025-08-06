public class PaymentMethodException : Exception
{
    public PaymentMethodException() : base("M�todo de pagamento inv�lido.") { }

    public PaymentMethodException(string message) : base(message) { }

    public PaymentMethodException(string message, Exception innerException)
        : base(message, innerException) { }
}
