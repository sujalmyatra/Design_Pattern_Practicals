using Practical_23.Application.Factories.Interfaces;
using Practical_23.Domain.Enums;

namespace Practical_23.Application.Abstract_Factory.Interfaces;

public interface IOverTimePayAbstractFactory
{
    bool CanHandle(Department department);
    IOverTimePayFactory GetFactory(Department deparment);
}
