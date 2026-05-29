using Practical_23.Application.Factories.Interfaces;
using Practical_23.Application.Interfaces;
using Practical_23.Application.Products;

namespace Practical_23.Application.Factories;

public class ITOverTimePayFactory : IOverTimePayFactory
{

    public IOverTimePay Create()
    {
        return new ITOverTimePay();
    }
}

