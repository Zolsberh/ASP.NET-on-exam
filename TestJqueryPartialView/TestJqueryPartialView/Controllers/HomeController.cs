﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestJqueryPartialView.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestLinck()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TestPV()
        {
            return PartialView();
        }

        public ActionResult TestIndex()
        {
            return PartialView();
        }
    }
}
