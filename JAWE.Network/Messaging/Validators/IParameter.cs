using System;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messaging.Validators
{
    public interface IParameter
    {
        Type ParameterType { get; }

        bool HasAttribute<T>() where T : AbstractMessageAttribute;
        T GetAttribute<T>() where T : AbstractMessageAttribute;

        ParameterValidationResult Validate(/*MessageProcessor processor, */IMessage target, string parameter);
    }
}