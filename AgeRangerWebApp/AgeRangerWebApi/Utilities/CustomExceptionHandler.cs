using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace AgeRangerWebApi.Utilities
{
    public class CustomExceptionHandler : ExceptionHandler
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void Handle(ExceptionHandlerContext context)
        {
            Logger.Debug(context.ExceptionContext.Exception.ToString());
        }
    }
}