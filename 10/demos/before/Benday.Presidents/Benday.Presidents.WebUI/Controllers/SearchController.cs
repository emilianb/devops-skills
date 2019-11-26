using Benday.Presidents.Core.Services;
using Benday.Presidents.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Benday.Presidents.Core.Models;

namespace Benday.Presidents.WebUI.Controllers
{
    public class SearchController : Controller
    {
        private IPresidentService _Service;

        public SearchController(IPresidentService service)
        {
            if (service == null)
                throw new ArgumentNullException("service", "service is null.");

            _Service = service;
        }

        // GET: Search
        public ActionResult Index()
        {
            var model = new SearchViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel model)
        {
            if (model == null)
            {
                throw new InvalidOperationException("Argument cannot be null.");
            }

            var results = _Service.Search(model.FirstName, model.LastName);

            var modelToReturn = new SearchViewModel();

            modelToReturn.FirstName = model.FirstName;
            modelToReturn.LastName = model.LastName;

            if (results != null)
            {
                Adapt(results, modelToReturn.Results);
            }            

            return View(modelToReturn);
        }

        private void Adapt(IList<President> fromValues, List<SearchResultRow> toValues)
        {
            if (fromValues == null)
                throw new ArgumentNullException("fromValues", "fromValues is null.");
            if (toValues == null)
                throw new ArgumentNullException("toValues", "toValues is null.");

            var adapter = new PresidentToSearchResultRowAdapter();

            SearchResultRow toValue;

            foreach (var fromValue in fromValues)
            {
                toValue = new SearchResultRow();

                adapter.Adapt(fromValue, toValue);

                toValues.Add(toValue);
            }
        }
    }
}