using Practical_23.Application.Abstract_Factory.Interfaces;
using Practical_23.Domain.Enums;
using System;
using System.Collections.Generic;
namespace Practical_23.Application.Abstract_Factory;

public class AbstractFactoryProvider 
{
    private readonly IEnumerable<IOverTimePayAbstractFactory> _factories;

    public AbstractFactoryProvider(IEnumerable<IOverTimePayAbstractFactory> factories) 
    { 
        _factories = factories; 
    }

    public IOverTimePayAbstractFactory GetFactory(Department department) 
    { 
        return _factories.First(x => x.CanHandle(department)); 
    }
}
