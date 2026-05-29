using Practical_23.Application.Interfaces;

namespace Practical_23.Application.Products;

public class OnSiteOverTimePay : IOverTimePay
{
    public decimal CalcultateOverTimePay(decimal hours)
    {
        return hours * 150;
    }
}
