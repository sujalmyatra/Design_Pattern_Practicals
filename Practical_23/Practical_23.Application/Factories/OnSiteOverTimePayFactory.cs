using Practical_23.Application.Factories.Interfaces;
using Practical_23.Application.Interfaces;
using Practical_23.Application.Products;

namespace Practical_23.Application.Factories;

public class OnSiteOverTimePayFactory : IOverTimePayFactory
{
    public IOverTimePay Create()
    {
        return new OnSiteOverTimePay();
    }
}
