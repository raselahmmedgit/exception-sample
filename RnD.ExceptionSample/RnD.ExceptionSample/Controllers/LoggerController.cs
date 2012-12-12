using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RnD.ExceptionSample.Helper;
using RnD.ExceptionSample.Models;
using RnD.ExceptionSample.ViewModels;

namespace RnD.ExceptionSample.Controllers
{
    public class LoggerController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Logger/

        public ActionResult Index()
        {
            return View();
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
