using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using BalticAmadeus.Container;

namespace FluentInterface.Examples.Classes
{
    internal class LoggingProxy : DynamicProxyBase
    {
        public static readonly List<string> LoggedMessages = new List<string>();

        public LoggingProxy(object target, Type type)
            : base(target, type)
        {
        }

        protected override IMessage InvokeProtected(IMethodCallMessage methodCall)
        {
            LoggedMessages.Add(string.Format("Calling method {0}...", methodCall.MethodName));
            Console.WriteLine("Calling overriden method {0}...", methodCall.MethodName);

            try
            {
                var result = methodCall.MethodBase.Invoke(Target, methodCall.InArgs);
                return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
            }
            catch (TargetInvocationException invocationException)
            {
                var exception = invocationException.InnerException;
                return new ReturnMessage(exception, methodCall);
            }
        }
    }
}