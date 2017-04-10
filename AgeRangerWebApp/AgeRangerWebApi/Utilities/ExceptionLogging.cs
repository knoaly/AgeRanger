using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace AgeRangerWebApi.Utilities
{
    public class ExceptionLogging : ExceptionLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void Log(ExceptionLoggerContext context)
        {
            //log exception to file
            Logger.Fatal(context.ExceptionContext.Exception.ToString());
        }
    }
}