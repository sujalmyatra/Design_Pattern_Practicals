using Practical_23.Application.Abstract_Factory.Interfaces;
using Practical_23.Application.Factories;
using Practical_23.Application.Factories.Interfaces;
using Practical_23.Domain.Enums;

namespace Practical_23.Application.Abstract_Factory;

public class OutdoorFactory : IOverTimePayAbstractFactory
{
    private readonly Dictionary<Department, IOverTimePayFactory> _factories;

    public OutdoorFactory(SalesOverTimePayFactory salesFactory, OnSiteOverTimePayFactory onsiteFactory)
    {
        _factories = new Dictionary<Department, IOverTimePayFactory>
        {   { Department.Sales, salesFactory },
            { Department.OnSite, onsiteFactory }
        };
    }

    public bool CanHandle(Department department) { return _factories.ContainsKey(department); }
    public IOverTimePayFactory GetFactory(Department department) { return _factories[department]; }
}