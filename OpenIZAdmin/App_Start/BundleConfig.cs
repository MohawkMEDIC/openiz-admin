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
 * Date: 2016-6-13
 */
using System.Web;
using System.Web.Optimization;

namespace OpenIZAdmin
{
	/// <summary>
	/// Represents bundle configuration for the application.
	/// </summary>
	public class BundleConfig
	{
		/// <summary>
		/// Registers bundles for the application.
		/// </summary>
		/// <param name="bundles">The bundle collection for which to add the bundles.</param>
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/moment").Include(
						"~/Scripts/moment.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
						"~/Scripts/toastr.min.js",
						"~/Scripts/toastr-logic.js"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
						"~/Content/bootstrap.min.css",
						"~/Content/bootstrap-theme.min.css",
						"~/Content/styles.css",
						"~/Content/toastr.min.css"));

			BundleTable.EnableOptimizations = true;
		}
	}
}
