﻿/*
 * Copyright 2016-2017 Mohawk College of Applied Arts and Technology
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
 * Date: 2016-7-24
 */

using OpenIZAdmin.Extensions;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace OpenIZAdmin.Filters
{
	/// <summary>
	/// Represents an action filter to change a users language.
	/// </summary>
	public class LanguageActionFilter : IActionFilter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LanguageActionFilter"/> class.
		/// </summary>
		public LanguageActionFilter()
		{
		}

		/// <summary>
		/// Called when the action is executed.
		/// </summary>
		/// <param name="filterContext">The filter context for the request.</param>
		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
		}

		/// <summary>
		/// Called when the action is executing.
		/// </summary>
		/// <param name="filterContext">The filter context for the request.</param>
		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var cultureInfo = new CultureInfo(GetLanguage(filterContext.HttpContext));

			Thread.CurrentThread.CurrentUICulture = cultureInfo;
		}

		/// <summary>
		/// Gets the users language preference based on a cookie.
		/// </summary>
		/// <param name="context">The HTTP context.</param>
		/// <returns>Returns the users preferred language.</returns>
		private static string GetLanguage(HttpContextBase context)
		{
			return context.GetCurrentLanguage();
		}
	}
}