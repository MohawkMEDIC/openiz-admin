﻿/*
 * Copyright 2016-2016 Mohawk College of Applied Arts and Technology
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you
 * may not use this file except in compliance with the License. You may
 * obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 *
 * User: Nityan
 * Date: 2016-7-30
 */

using OpenIZ.Core.Model.AMI.Auth;
using OpenIZ.Messaging.AMI.Client;
using OpenIZAdmin.Attributes;
using OpenIZAdmin.Localization;
using OpenIZAdmin.Models.PolicyModels;
using OpenIZAdmin.Models.PolicyModels.ViewModels;
using OpenIZAdmin.Services.Http;
using OpenIZAdmin.Services.Http.Security;
using OpenIZAdmin.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace OpenIZAdmin.Controllers
{
	/// <summary>
	/// Provides operations for administering policies.
	/// </summary>
	[TokenAuthorize]
	public class PolicyController : Controller
	{
		/// <summary>
		/// The internal reference to the <see cref="OpenIZ.Messaging.AMI.Client.AmiServiceClient"/> instance.
		/// </summary>
		private AmiServiceClient client;

		/// <summary>
		/// Initializes a new instance of the <see cref="OpenIZAdmin.Controllers.PolicyController"/> class.
		/// </summary>
		public PolicyController()
		{
		}

        /// <summary>
		/// Activates a policy.
		/// </summary>
		/// <param name="id">The id of the policy to be activated.</param>
		/// <returns>Returns the index view.</returns>
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Activate(string id)
        {
            Guid userKey = Guid.Empty;
            SecurityPolicyInfo policyInfo = null;
            
            if (PolicyUtil.IsValidString(id) && Guid.TryParse(id, out userKey))
            {
                try
                {
                    policyInfo = PolicyUtil.GetPolicy(this.client, userKey);

                    if (policyInfo == null)
                    {
                        TempData["error"] = Locale.Policy + " " +  Locale.NotFound;

                        return RedirectToAction("Index");
                    }
                    
                    policyInfo.Policy.ObsoletedBy = null;
                    policyInfo.Policy.ObsoletionTime = null;

                    this.client.UpdatePolicy(id, policyInfo);

                    TempData["success"] = Locale.Policy + " " + Locale.ActivatedSuccessfully;

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
#if DEBUG
                    Trace.TraceError("Unable to delete policy: {0}", e.StackTrace);
#endif
                    Trace.TraceError("Unable to delete policy: {0}", e.Message);
                }
            }

            TempData["error"] = Locale.UnableToActivate + " " + Locale.Policy;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the create policy view.
        /// </summary>
        /// <returns>Returns the create policy view.</returns>
        [HttpGet]
		public ActionResult Create()
		{
			CreatePolicyModel model = new CreatePolicyModel();

			model.GrantsList.Add(new SelectListItem { Text = Locale.Select, Value = "" });
			model.GrantsList.Add(new SelectListItem { Text = Locale.Deny, Value = "0" });
			model.GrantsList.Add(new SelectListItem { Text = Locale.Elevate, Value = "1" });
			model.GrantsList.Add(new SelectListItem { Text = Locale.Grant, Value = "2" });

			return View(model);
		}

		/// <summary>
		/// Creates a policy.
		/// </summary>
		/// <param name="model">The model containing the information about the policy.</param>
		/// <returns>Returns the index view.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreatePolicyModel model)
		{
			if (ModelState.IsValid)
			{
				SecurityPolicyInfo policy = PolicyUtil.ToSecurityPolicy(model);

				try
				{
					this.client.CreatePolicy(policy);

					TempData["success"] = Locale.Policy + " " +  Locale.CreatedSuccessfully;

					return RedirectToAction("Index");
				}
				catch (Exception e)
				{
#if DEBUG
					Trace.TraceError("Unable to create policy: {0}", e.StackTrace);
#endif
					Trace.TraceError("Unable to create policy: {0}", e.Message);
				}
			}

			TempData["error"] = "Unable to create policy";

			return View(model);
		}

		/// <summary>
		/// Deletes a policy.
		/// </summary>
		/// <param name="id">The id of the policy to be deleted.</param>
		/// <returns>Returns the index view.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(string id)
		{			            
            if (PolicyUtil.IsValidString(id))
            {
				try
				{
					this.client.DeletePolicy(id);
                    TempData["success"] = Locale.Policy + " " + Locale.DeletedSuccessfully;

                    return RedirectToAction("Index");
				}
				catch (Exception e)
				{
#if DEBUG
					Trace.TraceError("Unable to delete policy: {0}", e.StackTrace);
#endif
					Trace.TraceError("Unable to delete policy: {0}", e.Message);
				}
			}

            TempData["error"] = Locale.UnableToDelete + " " + Locale.Policy;


            return RedirectToAction("Index");
		}

		/// <summary>
		/// Dispose of any managed resources.
		/// </summary>
		/// <param name="disposing">Whether the current invocation is disposing.</param>
		protected override void Dispose(bool disposing)
		{
			Trace.TraceInformation("{0} disposing", nameof(PolicyController));

			this.client?.Dispose();

			base.Dispose(disposing);
		}

        [HttpGet]
        public ActionResult Edit(string key)
        {
            Guid policyId = Guid.Empty;
            SecurityPolicyInfo policyInfo = null;
            
            if(PolicyUtil.IsValidString(key) && Guid.TryParse(key, out policyId))
            {                
                policyInfo = PolicyUtil.GetPolicy(this.client, policyId);
                
                if (policyInfo == null)
                {
                    TempData["error"] = Locale.Policy + " " + Locale.NotFound;

                    return RedirectToAction("Index");
                }

                return View(PolicyUtil.ToEditPolicyModel(policyInfo));
            }

            TempData["error"] = Locale.Policy + " " + Locale.NotFound;

            return RedirectToAction("Index");
        }

        /// <summary>
		/// Updates a policy.
		/// </summary>
		/// <param name="model">The model containing the updated policy information.</param>
        /// <param name="key">The policy guid indentifier.</param>
		/// <returns>Returns the index view.</returns>
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPolicyModel model)
        {            
            if (ModelState.IsValid)
            {                
                try
                {
                    SecurityPolicyInfo policy = PolicyUtil.GetPolicy(this.client, model.Key);
                   
                    if(policy == null)
                    { 
                        TempData["error"] = Locale.Policy + " " + Locale.NotFound;

                        return RedirectToAction("Index");
                    }


                    SecurityPolicyInfo policyInfo = PolicyUtil.ToSecurityPolicy(model, policy);                    
                                       
                    this.client.UpdatePolicy(model.Key.ToString(), policyInfo);

                    TempData["success"] = Locale.Policy + " " + Locale.UpdatedSuccessfully;

                    return RedirectToAction("Index");         
                                                                     
                }
                catch (Exception e)
                {
#if DEBUG
                    Trace.TraceError("Unable to edit policy: {0}", e.StackTrace);
#endif
                    Trace.TraceError("Unable to edit policy: {0}", e.Message);
                }
            }

            TempData["error"] = Locale.UnableToUpdate + " " + Locale.Policy;

            return View(model);
        }

        [HttpGet]
		public ActionResult Index()
		{
			TempData["searchType"] = "Policy";
			return View(PolicyUtil.GetAllPolicies(this.client));
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var restClient = new RestClientService(Constants.AMI);

			restClient.Accept = "application/xml";
			restClient.Credentials = new AmiCredentials(this.User, HttpContext.Request);

			this.client = new AmiServiceClient(restClient);

			base.OnActionExecuting(filterContext);
		}

		[HttpGet]
		public ActionResult Search(string searchTerm)
		{
			IEnumerable<PolicyViewModel> policies = new List<PolicyViewModel>();

			try
			{				
                if(PolicyUtil.IsValidString(searchTerm))
                {
					var collection = this.client.GetPolicies(p => p.Name.Contains(searchTerm));

					TempData["searchTerm"] = searchTerm;

					return PartialView("_PolicySearchResultsPartial", collection.CollectionItem.Select(p => PolicyUtil.ToPolicyViewModel(p)));
				}
			}
			catch (Exception e)
			{
#if DEBUG
				Trace.TraceError("Unable to search policies: {0}", e.StackTrace);
#endif
				Trace.TraceError("Unable to search policies: {0}", e.Message);
			}

            TempData["error"] = Locale.InvalidSearch;
			TempData["searchTerm"] = searchTerm;

			return PartialView("_PoliciesPartial", policies);
		}

		[HttpGet]
		public ActionResult ViewPolicy(string key)
		{
			Guid policyId = Guid.Empty;
			
            if(PolicyUtil.IsValidString(key) && Guid.TryParse(key, out policyId))
            {
				var result = this.client.GetPolicies(r => r.Key == policyId);

				if (result.CollectionItem.Count == 0)
				{
					TempData["error"] = Locale.Policy + " " + Locale.NotFound;

					return RedirectToAction("Index");
				}

				return View(PolicyUtil.ToPolicyViewModel(result.CollectionItem.Single()));
			}

			TempData["error"] = Locale.Policy + " " + Locale.NotFound;

            return RedirectToAction("Index");
		}
	}
}