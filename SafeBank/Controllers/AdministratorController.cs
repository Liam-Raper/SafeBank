using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Business.Interfaces;
using Business.Models;
using SafeBank.Models;

namespace SafeBank.Controllers
{
    public class AdministratorController : Controller
    {

        private IOrganisationService _organisationService;
        private IBranchService _branchService;
        private IBankService _bankService;

        public AdministratorController(IOrganisationService organisationService, IBranchService branchService, IBankService bankService)
        {
            _organisationService = organisationService;
            _branchService = branchService;
            _bankService = bankService;
        }

        public ActionResult Dashboard()
        {
            var model = new AdministratorDashboardDetails
            {
                OrganisationsDetailses = _organisationService.GetOrganisations()
                    .Select(x => new OrganisationDetails {Id = x.Id, Name = x.Name, Code = x.Code, CanDelete = x.BranchCount == 0})
                    .ToArray()
            };
            return View(model);
        }

        public ActionResult AddOrganisation()
        {
            return View(new AddOrganisationDetails());
        }

        [HttpPost]
        public ActionResult AddOrganisation(AddOrganisationDetails model)
        {
            if (!ModelState.IsValid || _organisationService.OrganisationExist(model.Name) ||
                _organisationService.OrganisationCodeExist(model.Code ?? 0)) return View(model);
            _organisationService.AddOrganisation(new OrganisationBO
            {
                Name = model.Name,
                Code = model.Code ?? 0
            });
            return RedirectToAction("Dashboard");
        }

        public ActionResult EditOrganisation(int organisationId)
        {
            if (!_organisationService.OrganisationIdExists(organisationId)) return RedirectToAction("Dashboard");
            var model = new EditOrganisationDetails();
            var org = _organisationService.GetOrganisation(organisationId);
            model.Id = org.Id;
            model.Name = org.Name;
            model.Code = org.Code;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditOrganisation(EditOrganisationDetails model)
        {
            if (!ModelState.IsValid || !_organisationService.OrganisationIdExists(model.Id)) return View(model);
            _organisationService.UpdateOrganisation(new OrganisationBO {Id = model.Id,Code = model.Code ?? 0, Name = model.Name});
            return RedirectToAction("Dashboard");
        }

        public ActionResult DeleteOrganisation(int organisationId)
        {
            if (_organisationService.OrganisationIdExists(organisationId))
            {
                _organisationService.DeleteOrganisation(organisationId);
            }
            return RedirectToAction("Dashboard");
        }

        public ActionResult OrganisationBranchesList(int organisationId)
        {
            var model = new BranchesDetails
            {
                OrganisationId = organisationId,
                BranchDetailses =
                    _branchService.GetAllBranchesAtAnOrganisation(organisationId).Select(x => new BranchDetails
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code
                    })
            };
            return View(model);
        }

        public ActionResult AddBranch(int organisationId)
        {
            var model = new AddBranchDetails {OrganisationId = organisationId};
            return View(model);
        }
        
        [HttpPost]
        public ActionResult AddBranch(AddBranchDetails model)
        {
            if (!ModelState.IsValid || _branchService.BranchExist(model.OrganisationId, model.Name) ||
                _branchService.BranchCodeExist(model.OrganisationId, model.Code ?? 0)) return View(model);
            _branchService.AddBranch(model.OrganisationId,new BranchBO
            {
                Name = model.Name,
                Code = model.Code ?? 0
            });
            return RedirectToAction("OrganisationBranchesList",new { organisationId = model.OrganisationId });
        }

        public ActionResult EditBranch(int branchId, int organisationId)
        {
            if (!_branchService.BranchIdExists(branchId)) return RedirectToAction("OrganisationBranchesList",new { organisationId });
            var model = new EditBranchDetails();
            var branch = _branchService.GetBranch(branchId);
            model.Id = branch.Id;
            model.Name = branch.Name;
            model.Code = branch.Code;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditBranch(EditBranchDetails model)
        {
            if (!ModelState.IsValid || !_branchService.BranchIdExists(model.Id)) return View(model);
            _branchService.UpdateBranch(new BranchBO { Id = model.Id, Code = model.Code ?? 0, Name = model.Name });
            return RedirectToAction("OrganisationBranchesList",
                new {organisationId = _branchService.GetOrganisationId(model.Id)});
        }

        public ActionResult DeleteBranch(int branchId, int organisationId)
        {
            _branchService.DeleteBranch(branchId);
            return RedirectToAction("OrganisationBranchesList", new {organisationId});
        }

        public ActionResult BankesList(int branchId)
        {
            var model = new BankesDetails
            {
                BranchId = branchId,
                OrganisationId = _branchService.GetOrganisationId(branchId),
                BankDetailses =
                    _bankService.GetAllBanksUnderABranch(branchId).Select(x => new BankDetails
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code
                    })
            };
            return View(model);
        }

        public ActionResult AddBank(int branchId)
        {
            var model = new AddBankDetails { BranchId = branchId };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddBank(AddBankDetails model)
        {
            if (!ModelState.IsValid || _bankService.BankExist(model.BranchId, model.Name) ||
                _bankService.BankCodeExist(model.BranchId, model.Code ?? 0)) return View(model);
            _bankService.AddBank(model.BranchId, new BankBO
            {
                Name = model.Name,
                Code = model.Code ?? 0
            });
            return RedirectToAction("BankesList", new { branchId = model.BranchId });
        }
        
    }
}