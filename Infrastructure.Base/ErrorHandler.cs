using Castle.DynamicProxy;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xrm.Infrastructure.Base
{
    public class ErrorHandler : IInterceptor
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                logger.Error(ex, invocation.Method.Name, invocation.Arguments);
            }
        }
    }
}
