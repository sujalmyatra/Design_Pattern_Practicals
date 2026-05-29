using Practical_23.Application.Interfaces;

namespace Practical_23.Application.Products;

public class HROverTimePay : IOverTimePay
{
    public decimal CalcultateOverTimePay(decimal hours)
    {
        return hours * 170;
    }
}
