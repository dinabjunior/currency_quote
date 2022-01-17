namespace System.ComponentModel.DataAnnotations
{
    using System;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequireNonDefaultAttribute : ValidationAttribute
    {
        public RequireNonDefaultAttribute() : base("The {0} field is required.") { }

        public override bool IsValid(object value)
        {
            return value is null || !value.GetType().IsValueType || !Equals(value, Activator.CreateInstance(value.GetType()));
        }
    }
}
