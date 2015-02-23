using System;

namespace CcAcca.DemoUpstream
{
    /// <summary>
    ///     Used to annotate a model class indicating that instances are reference data as opposed
    ///     to transactional data
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ReferenceDataAttribute : Attribute
    {
    }
}