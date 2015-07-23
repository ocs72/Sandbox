using BO;
using Luchini.Models;
using System;
using System.Web.Mvc;

namespace Luchini.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBenefitCalculator _benefitCalculator;

        public HomeController(IBenefitCalculator benefitCalculator)
        {
            _benefitCalculator = benefitCalculator;
            if (_benefitCalculator == null)
            {
                throw new Exception("BenefitCalculator dependency is null");
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Benefits()
        {
            //BenefitModel bm = new BenefitModel();
            //bm.Dependents.Add("yiu nmer");

            //return View(bm);

            return View(new BenefitModel());
        }

        public JsonResult Calculate(BenefitModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult();
            }

            BenefitResults benefits = _benefitCalculator.CalculateBenefits(model.Employee, model.Dependents);

            return Json(new { BenefitYear = benefits.BenefitYear, BenefitCheck = benefits.BenefitCheck, TotalYear = benefits.TotalYear, TotalCheck = benefits.TotalCheck });
        }
    }
}