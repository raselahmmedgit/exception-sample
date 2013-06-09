using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RnD.ExceptionSample.Helper;
using RnD.ExceptionSample.Models;
using RnD.ExceptionSample.ViewModels;
using log4net;

namespace RnD.ExceptionSample.Controllers
{
    public class LoggerController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        private readonly ILog _log = LogManager.GetLogger("RollingFileAppender");

        //private readonly ILog _log = LogManager.GetLogger("log4netFileAppender");

        //
        // GET: /Logger/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TextFile()
        {
            /*
             
                 <appender name="LogFileAppender" type="log4net.Appender.RollingFileAppender" >
                    <param name="File" value="App_Data\log4net.txt" />
                    <param name="AppendToFile" value="true" />
                    <rollingStyle value="Size" />
                    <maxSizeRollBackups value="10" />
                    <maximumFileSize value="10MB" />
                    <staticLogFileName value="true" />
                    <layout type="log4net.Layout.PatternLayout">
                    <param name="ConversionPattern" value="%date%newline %-5level - %message%newline%newline%newline" />
                    </layout>
                  </appender>
             
                 <root>
                    <level value="ALL" />
                    <appender-ref ref="LogFileAppender" />
                 </root>
              
             */

            var commonMess = "Common Message for all";
            ViewBag.CommonMess = commonMess;
            return View("CommonMess");
        }

        //
        // GET: /Logger/GetLoggers/

        public ActionResult GetLoggers(DataTableParamModel param)
        {
            var loggers = _db.Loggers.ToList();

            var viewLoggers = loggers.Select(log => new LoggerTableModels() { LoggerId = Convert.ToString(log.LoggerId), Summery = log.Summery, Details = log.Details, FilePath = log.FilePath, Url = log.Url, LoggerTypeName = log.LoggerType == null ? null : Convert.ToString(log.LoggerType.LoggerTypeName) });

            IEnumerable<LoggerTableModels> filteredLoggers;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredLoggers = viewLoggers.Where(pro => (pro.Summery ?? "").Contains(param.sSearch) || (pro.Details ?? "").Contains(param.sSearch) || (pro.LoggerTypeName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredLoggers = viewLoggers;
            }

            var viewOdjects = filteredLoggers.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from logMdl in viewOdjects
                         select new[] { logMdl.LoggerId, logMdl.Summery, logMdl.Details, logMdl.FilePath, logMdl.Url, logMdl.LoggerTypeName };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = loggers.Count(),
                iTotalDisplayRecords = filteredLoggers.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

    }
}
