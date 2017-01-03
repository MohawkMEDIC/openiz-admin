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
 * Date: 2016-5-31
 */

using Microsoft.AspNet.Identity;
using OpenIZ.Core.Model.AMI.Diagnostics;
using OpenIZAdmin.Attributes;
using OpenIZAdmin.Localization;
using OpenIZAdmin.Models;
using OpenIZAdmin.Models.CertificateModels.ViewModels;
using OpenIZAdmin.Models.DebugModels.ViewModels;
using OpenIZAdmin.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace OpenIZAdmin.Controllers
{
	/// <summary>
	/// Provides operations for generic use.
	/// </summary>
	[TokenAuthorize]
	public class HomeController : BaseController
	{
		/// <summary>
		/// Displays the index view.
		/// </summary>
		/// <returns>Returns the index view.</returns>
		public ActionResult Index()
		{
			if (!RealmConfig.IsJoinedToRealm())
			{
				return RedirectToAction("JoinRealm", "Realm");
			}

			DashboardViewModel viewModel = new DashboardViewModel
			{
				Applets = AppletUtil.GetApplets(this.AmiClient),
				CertificateRequests = new List<CertificateSigningRequestViewModel>(), //CertificateUtil.GetAllCertificateSigningRequests(this.client),
				Devices = DeviceUtil.GetAllDevices(this.AmiClient).OrderBy(d => d.CreationTime).ThenBy(d => d.Name).Take(15),
				Roles = RoleUtil.GetAllRoles(this.AmiClient).OrderBy(r => r.Name).Take(15),
				Users = UserUtil.GetAllUsers(this.AmiClient).OrderBy(u => u.Username).Take(15)
			};

			return View(viewModel);
		}

        /// <summary>
        /// Gets the current user info and initiates the bug report page
        /// </summary>        
        /// <returns>Returns the SubmitBugReport view.</returns>
        [HttpGet]
        public ActionResult SubmitBugReport()
        {
            try
            {
                var userId = Guid.Parse(User.Identity.GetUserId());

                var userEntity = this.AmiClient.GetUser(userId.ToString());

                if (userEntity == null)
                {
                    TempData["error"] = Locale.User + " " + Locale.NotFound;

                    return RedirectToAction("Index");
                }

                var model = HomeUtil.ToSubmitBugReport(userEntity);

                return View(model);
            }
            catch(Exception e)
            {
#if DEBUG
                Trace.TraceError("Unable to retrieve current user", e.StackTrace);
#endif
                Trace.TraceError("Unable to retrieve current user", e.Message);
            }

            TempData["error"] = Locale.User + " " + Locale.NotFound;

            return RedirectToAction("Index");
        }


        /// <summary>
		/// Displays the create view.
		/// </summary>
		/// <param name="model">The model containing the bug report information.</param>
		/// <returns>Returns the Index view.</returns>
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitBugReport(SubmitBugReportViewModel model)
        {
            if (ModelState.IsValid)
            {                
                try
                {
                    DiagnosticReport report = HomeUtil.ToDiagnosticReport(this.ImsiClient, model);
                    report = AmiClient.SubmitDiagnosticReport(report);                    
                    model.TransactionMessage = "Bug report id:" + " " + report.CorrelationId;

                    //TempData["success"] = Locale.Role + " " + Locale.Updated + " " + Locale.Successfully;

                    //return RedirectToAction("SubmitBugReport");
                }
                catch (Exception e)
                {
#if DEBUG
                    Trace.TraceError("Unable to submit bug report: {0}", e.StackTrace);
#endif
                    Trace.TraceError("Unable to submit bug report: {0}", e.Message);
                }
            }
            
            return View(model);
        }

        /// <summary>
        /// Gets the version information of the current application.
        /// </summary>
        /// <returns>Returns the version information.</returns>
        [HttpGet]
		[TokenAuthorize]
		public ActionResult VersionInformation()
		{
			var viewModel = new VersionViewModel(typeof(MvcApplication).Assembly);

			viewModel.Assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies().Select(a => new AssemblyInfoViewModel(a)).Where(a => a.Title != null).OrderBy(a => a.Title));

			return View(viewModel);
		}
	}
}