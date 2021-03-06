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
 * User: Andrew
 * Date: 2017-5-10
 */

using OpenIZ.Core.Model.Constants;
using OpenIZ.Core.Model.Entities;
using OpenIZAdmin.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OpenIZAdmin.Models.Core
{
	/// <summary>
	/// Represents a model of a base user profile.
	/// </summary>
	public abstract class UserModelBase
	{
		/// <summary>
		/// Gets or sets the email address of the user.
		/// </summary>
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email", ResourceType = typeof(Locale))]
		[EmailAddress(ErrorMessageResourceName = "InvalidEmailAddress", ErrorMessageResourceType = typeof(Locale))]
		[StringLength(256, ErrorMessageResourceName = "EmailLength256", ErrorMessageResourceType = typeof(Locale))]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the id of the facility of the user.
		/// </summary>
		[Display(Name = "Facility", ResourceType = typeof(Locale))]
		public string Facility { get; set; }

		/// <summary>
		/// Gets or sets the givens names of the user.
		/// </summary>
		[Display(Name = "GivenName", ResourceType = typeof(Locale))]
		[Required(ErrorMessageResourceName = "GivenNameRequired", ErrorMessageResourceType = typeof(Locale))]
		public string GivenName { get; set; }

		/// <summary>
		/// Gets or sets the phone number of the user.
		/// </summary>
		[DataType(DataType.PhoneNumber)]
		[Display(Name = "Phone", ResourceType = typeof(Locale))]
		[Required(ErrorMessageResourceName = "PhoneNumberRequired", ErrorMessageResourceType = typeof(Locale))]
		[StringLength(25, ErrorMessageResourceName = "PhoneNumberTooLong", ErrorMessageResourceType = typeof(Locale))]
		[RegularExpression(Constants.RegExPhoneNumberTanzania, ErrorMessageResourceName = "InvalidPhoneNumber", ErrorMessageResourceType = typeof(Locale))]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Gets or sets the phone type of the user.
		/// </summary>
		[Display(Name = "PhoneType", ResourceType = typeof(Locale))]
		[Required(ErrorMessageResourceName = "PhoneTypeRequired", ErrorMessageResourceType = typeof(Locale))]
		public string PhoneType { get; set; }

		/// <summary>
		/// Gets or sets the types of phones.
		/// </summary>
		public List<SelectListItem> PhoneTypeList { get; set; }

		/// <summary>
		/// Gets or sets the family names of the user.
		/// </summary>
		[Display(Name = "Surname", ResourceType = typeof(Locale))]
		[Required(ErrorMessageResourceName = "SurnameRequired", ErrorMessageResourceType = typeof(Locale))]
		public string Surname { get; set; }

		/// <summary>
		/// Gets or sets the username of the user.
		/// </summary>
		[Display(Name = "Username", ResourceType = typeof(Locale))]
		public string Username { get; set; }

		/// <summary>
		/// Checks if the Phone Number and Type input contains an entry
		/// </summary>
		/// <returns>Returns true if a number and type exists, false if no phone number or type is assigned</returns>
		public bool HasPhoneNumberAndType() => !string.IsNullOrWhiteSpace(PhoneNumber) && !string.IsNullOrWhiteSpace(PhoneType);

		/// <summary>
		/// Checks if the facility selected is different than the one currently assigned to the user
		/// </summary>
		/// <returns>Returns true if a new facility is selected, false if selection hasn't changed</returns>
		public bool HasSelectedNewFacility(UserEntity userEntity, Guid? facilityId)
		{
			return userEntity.Relationships.Find(r => r.RelationshipTypeKey == EntityRelationshipTypeKeys.DedicatedServiceDeliveryLocation && r.TargetEntityKey == facilityId) == null;
		}

		/// <summary>
		/// Determines whether the name is between 1 and 100 characters.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns><c>true</c> if the name is between 1 and 100 characters; otherwise, <c>false</c>.</returns>
		/// <exception cref="System.ArgumentNullException">name</exception>
		public bool IsValidNameLength(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name), Locale.ValueCannotBeNull);
			}

			return name.Length > 0 && name.Length <= 100;
		}
	}
}