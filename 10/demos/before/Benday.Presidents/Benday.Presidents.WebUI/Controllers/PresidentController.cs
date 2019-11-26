using Benday.Presidents.Common;
using Benday.Presidents.Core;
using Benday.Presidents.Core.Factories;
using Benday.Presidents.Core.Models;
using Benday.Presidents.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Benday.Presidents.WebUI.Controllers
{
    public class PresidentController : Controller
    {
        private const int ID_FOR_CREATE_NEW_PRESIDENT = 0;
        private IPresidentService _Service;

        public PresidentController(IPresidentService service)
        {
            if (service == null)
                throw new ArgumentNullException("service", "service is null.");

            _Service = service;
        }

        public ActionResult Index()
        {
            var presidents = _Service.GetPresidents();

            return View(presidents);
        }

        public ActionResult Details(int? id)
        {
            if (id == null || id.HasValue == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var president = _Service.GetPresidentById(id.Value);

            if (president == null)
            {
                return HttpNotFound();
            }            

            return View(president);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit", new { id = ID_FOR_CREATE_NEW_PRESIDENT });
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            President president;

            if (id.Value == ID_FOR_CREATE_NEW_PRESIDENT)
            {
                // create new
                president = new President();
                president.AddTerm(PresidentsConstants.President, 
                    default(DateTime), 
                    default(DateTime), 0);
            }
            else
            {
                president = _Service.GetPresidentById(id.Value);
            }            

            if (president == null)
            {
                return HttpNotFound();
            }

            return View(president);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(President president)
        {
            if (ModelState.IsValid)
            {
                bool isCreateNew = false;

                if (president.Id == ID_FOR_CREATE_NEW_PRESIDENT)
                {
                    isCreateNew = true;
                }
                else
                {
                    President toValue =
                        _Service.GetPresidentById(president.Id);

                    if (toValue == null)
                    {
                        return new HttpStatusCodeResult(
                            HttpStatusCode.BadRequest,
                            String.Format("Unknown president id '{0}'.", president.Id));
                    }
                }

                _Service.Save(president);

                if (isCreateNew == true)
                {
                    RedirectToAction("Edit", new { id = president.Id });
                }
                else
                {
                    return RedirectToAction("Edit");
                }                
            }

            return View(president);
        }

        public ActionResult ResetDatabase()
        {
            var utility = new TestDataUtility(_Service);

            utility.CreatePresidentTestData();

            return RedirectToAction("Index");
        }
    }
}