using Practical_23.Application.Interfaces;

namespace Practical_23.Application.Products;

public class SalesOvertimePay : IOverTimePay
{
    public decimal CalcultateOverTimePay(decimal hours)
    {
        return hours * 100;
    }
}
