using Practical_23.Application.Abstract_Factory.Interfaces;
using Practical_23.Application.Factories;
using Practical_23.Application.Factories.Interfaces;
using Practical_23.Domain.Enums;

namespace Practical_23.Application.Abstract_Factory;

public class IndoorFactory : IOverTimePayAbstractFactory
{
    private readonly Dictionary<Department, IOverTimePayFactory> _factories;

    public IndoorFactory(ITOverTimePayFactory itFactory, HROverTimePayFactory hrFactory)
    {
        _factories = new Dictionary<Department, IOverTimePayFactory>
        {
            { Department.IT, itFactory },
            { Department.HR, hrFactory }
        };
    }
    public bool CanHandle(Department department) { return _factories.ContainsKey(department); }
    public IOverTimePayFactory GetFactory(Department department) { return _factories[department]; }
}
