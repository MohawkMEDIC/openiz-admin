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
 * User: khannan
 * Date: 2016-11-14
 */

using OpenIZAdmin.Attributes;
using OpenIZAdmin.Localization;
using OpenIZAdmin.Models.ApplicationModels;
using OpenIZAdmin.Models.ApplicationModels.ViewModels;
using OpenIZAdmin.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Linq;
using OpenIZ.Core.Model.Security;
using OpenIZ.Core.Model.AMI.Auth;

namespace OpenIZAdmin.Controllers
{
	/// <summary>
	/// Provides operations for managing security applications.
	/// </summary>
	[TokenAuthorize]
	public class ApplicationController : BaseController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationController"/> class.
		/// </summary>
		public ApplicationController()
		{
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateApplicationModel model)
		{
			if (ModelState.IsValid)
			{
                try
                {
                    var application = this.AmiClient.CreateApplication(ApplicationUtil.ToSecurityApplication(model));

                    return RedirectToAction("ViewApplication", new { key = application.Id.ToString() });
                }
                catch (Exception e)
                {
#if DEBUG
                    Trace.TraceError("Unable to create application: {0}", e.StackTrace);
#endif
                    Trace.TraceError("Unable to create application: {0}", e.Message);
                }
            }

			TempData["error"] = Locale.UnableToCreate + " " + Locale.SecurityApplication;

			return View(model);
		}

        /// <summary>
        /// Gets the application object to edit
        /// </summary>
        /// <param name="key">The id of the application to be edited.</param>
        /// <returns>Returns the Edit view.</returns>
        [HttpGet]
        public ActionResult Edit(string key)
        {
            Guid appKey = Guid.Empty;
            SecurityApplicationInfo application = null;

            if (ApplicationUtil.IsValidString(key) && Guid.TryParse(key, out appKey))
            {
                application = ApplicationUtil.GetApplication(this.AmiClient, appKey);

                if (application == null)
                {
                    TempData["error"] = Locale.Application + " " + Locale.NotFound;

                    return RedirectToAction("Index");
                }

                return View(ApplicationUtil.ToEditApplicationModel(this.AmiClient, application));
            }

            TempData["error"] = Localization.Locale.Device + " " + Locale.NotFound;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Appies the changes to the device object
        /// </summary>
        /// <param name="model">The model containing the updated device information.</param>
        /// <returns>Returns the ViewDevice view.</returns>
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditApplicationModel model)
        {
            if (ModelState.IsValid)
            {
                //try
                //{                    
                if (ModelState.IsValid)
                {

                    //var deviceEntity = DeviceUtil.GetDevice(this.AmiClient, model.Id);

                    //if (deviceEntity == null)
                    //{
                    //    TempData["error"] = Locale.Device + " " + Locale.NotFound;

                    //    return RedirectToAction("Index");
                    //}

                    //List<SecurityPolicy> addPolicies = new List<SecurityPolicy>();

                    //if (model.AddPoliciesList != null && model.AddPoliciesList.Count() > 0)
                    //{
                    //    addPolicies = DeviceUtil.GetNewPolicies(this.AmiClient, model.AddPoliciesList);
                    //}

                    //SecurityDeviceInfo deviceInfo = DeviceUtil.ToSecurityDeviceInfo(model, deviceEntity, addPolicies);

                    //this.AmiClient.UpdateDevice(model.Id.ToString(), deviceInfo);

                    //TempData["success"] = Locale.Device + " " + Locale.UpdatedSuccessfully;

                    TempData["error"] = Locale.UnableToUpdate + " " + Locale.Device;

                    return Redirect("Index");
                }
                else
                {
                    return View(model);
                }
            }

            TempData["error"] = Locale.UnableToUpdate + " " + Locale.Device;

            return View(model);
        }

        /// <summary>
        /// Displays the index view.
        /// </summary>
        /// <returns>Returns the index view.</returns>
        [HttpGet]
		public ActionResult Index()
		{
			TempData["searchType"] = "Application";
			return View();
		}

        /// <summary>
        /// Gets an application list based on the search parameter applied to the SoftwareName field
        /// </summary>
        /// <param name="searchTerm">The search parameter to apply to the query.</param>
        /// <returns>Returns the index view.</returns>
		[HttpGet]
        public ActionResult Search(string searchTerm)
        {
            IEnumerable<ApplicationViewModel> applications = new List<ApplicationViewModel>();

            try
            {
                if (ApplicationUtil.IsValidString(searchTerm))
                {
                    var collection = this.AmiClient.GetApplications(d => d.Name.Contains(searchTerm));

                    TempData["searchTerm"] = searchTerm;
                    
                    return PartialView("_ApplicationsPartial", collection.CollectionItem.Select(d => ApplicationUtil.ToApplicationViewModel(d)));
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Trace.TraceError("Unable to search applications: {0}", e.StackTrace);
#endif
                Trace.TraceError("Unable to search applications: {0}", e.Message);
            }

            TempData["error"] = Locale.InvalidSearch;
            TempData["searchTerm"] = searchTerm;

            return PartialView("_ApplicationsPartial", applications);
        }

        /// <summary>
        /// Searches for an application object with the supplied id.
        /// </summary>
        /// <param name="id">The application identifier search parameter to apply to the query.</param>
        /// <returns>Returns the ViewApplication view.</returns>
        [HttpGet]
		public ActionResult ViewApplication(string id)
		{            
            if (ApplicationUtil.IsValidString(id)) 
            {
                var result = ApplicationUtil.GetApplication(this.AmiClient, id);

                if (result == null)
                {
                    TempData["error"] = Locale.Application + " " + Locale.NotFound;

                    return RedirectToAction("Index");
                }

                return View(ApplicationUtil.ToApplicationViewModel(result));
            }

            TempData["error"] = Locale.Application + " " + Locale.NotFound;

            return RedirectToAction("Index");
        }
	}
}