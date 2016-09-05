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
 * Date: 2016-8-14
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OpenIZAdmin.Models.UserModels
{
	public class EditUserModel
	{
		public EditUserModel()
		{
			this.FacilityList = new List<SelectListItem>();
			this.FamilyNameList = new List<SelectListItem>();
			this.FamilyNames = new List<string>();
			this.GivenNamesList = new List<SelectListItem>();
			this.GivenNames = new List<string>();
			this.RolesList = new List<SelectListItem>();
		}

		/// <summary>
		/// Gets or sets the email address of the user.
		/// </summary>
		[Display(Name = "Email", ResourceType = typeof(Localization.Locale))]
		[Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Localization.Locale))]
		[EmailAddress(ErrorMessageResourceName = "InvalidEmailAddress", ErrorMessageResourceType = typeof(Localization.Locale))]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the id of the facility of the user.
		/// </summary>
		[Display(Name = "Facility", ResourceType = typeof(Localization.Locale))]
		[Required(ErrorMessageResourceName = "FacilityRequired", ErrorMessageResourceType = typeof(Localization.Locale))]
		public string FacilityId { get; set; }

		/// <summary>
		/// Gets or sets the list of facilities.
		/// </summary>
		public List<SelectListItem> FacilityList { get; set; }

		public List<SelectListItem> FamilyNameList { get; set; }

		/// <summary>
		/// Gets or sets the family names of the user.
		/// </summary>
		[Display(Name = "FamilyNames", ResourceType = typeof(Localization.Locale))]
		public List<string> FamilyNames { get; set; }

		public List<SelectListItem> GivenNamesList { get; set; }

		/// <summary>
		/// Gets or sets the givens names of the user.
		/// </summary>
		[Display(Name = "Names", ResourceType = typeof(Localization.Locale))]
		public List<string> GivenNames { get; set; }

		/// <summary>
		/// Gets or sets the password of the user.
		/// </summary>
		[DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(Localization.Locale))]
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the roles of the user.
		/// </summary>
		[Display(Name = "Roles", ResourceType = typeof(Localization.Locale))]
		[Required(ErrorMessageResourceName = "RolesRequired", ErrorMessageResourceType = typeof(Localization.Locale))]
		public IEnumerable<string> Roles { get; set; }

		/// <summary>
		/// Gets or sets the list of roles.
		/// </summary>
		public List<SelectListItem> RolesList { get; set; }

		/// <summary>
		/// Gets or sets the username of the user.
		/// </summary>
		[Display(Name = "Username", ResourceType = typeof(Localization.Locale))]
		[Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(Localization.Locale))]
		[StringLength(255, ErrorMessageResourceName = "UsernameTooLong", ErrorMessageResourceType = typeof(Localization.Locale))]
		public string Username { get; set; }
	}
}