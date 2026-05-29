using Practical_23.Application.Interfaces;

namespace Practical_23.Application.Products;

public class ITOverTimePay : IOverTimePay
{
    public decimal CalcultateOverTimePay(decimal hours)
    {
        return hours * 200;
    }
}
