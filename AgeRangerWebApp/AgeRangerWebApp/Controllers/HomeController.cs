using AgeRangerBO.Managers;
using AgeRangerBO.Models;
using AgeRangerBO.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AgeRangerWebApp.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IPeopleManager _peopleManager;

        public HomeController(IPeopleManager peopleManager)
        {
            _peopleManager = peopleManager;
        }

        public async Task<ActionResult> Index()
        {
            var vm = new PeopleViewModel(_peopleManager);
            await vm.GetEntityList();
            return View("Index", vm);
        }

        [HttpPost]
        public async Task<ActionResult> Index(PeopleViewModel formVm)
        {
            var vm = new PeopleViewModel(_peopleManager);
            if (formVm.EventCommand == "search")
            {
                vm.EntitySearch = formVm.EntitySearch;
                await vm.GetEntityByName();
            }
            else
            {
                if (formVm.EventCommand == "delete")
                {
                    await vm.DeleteEntity(long.Parse(formVm.EventArgument));
                }

                //for reset search too as both needed GetAll
                ModelState.Clear();
                await vm.GetEntityList();
            }
            return View("Index", vm);
        }

        public async Task<ActionResult> Details(long id)
        {
            var vm = new PeopleViewModel(_peopleManager);
            if (id == -1)
            {
                vm.SetupDetailForCreate();
            }
            else
            {
                await vm.SetupDetailForEdit(id);
            }
            return PartialView("_Details", vm.EntityDetail);
        }

        [HttpPost]
        public async Task<ActionResult> Details([Bind(Include = "Id,FirstName,LastName,Age")] Person entity)
        {
            if (ModelState.IsValid)
            {
                var vm = new PeopleViewModel(_peopleManager);
                vm.EntityDetail = entity;
                bool result = await vm.SaveEntity();

                if (result)
                {
                    ModelState.Clear();
                    return Json(new { success = true });
                }
            }
            return PartialView("_Details", entity);
        }

        public ActionResult Error404()
        {
            return View();
        }
    }
}