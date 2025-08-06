public class PaymentMethodException : Exception
{
    public PaymentMethodException() : base("Método de pagamento inválido.") { }

    public PaymentMethodException(string message) : base(message) { }

    public PaymentMethodException(string message, Exception innerException)
        : base(message, innerException) { }
}
