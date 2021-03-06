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
 * Date: 2016-7-8
 */

using OpenIZ.Core.Model.AMI.DataTypes;
using OpenIZAdmin.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OpenIZAdmin.Models.AssigningAuthorityModels
{
	/// <summary>
	/// Represents an assigning authority view model.
	/// </summary>
	public class AssigningAuthorityViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AssigningAuthorityViewModel"/> class.
		/// </summary>
		public AssigningAuthorityViewModel()
		{
			AuthorityScopeList = new List<AuthorityScopeViewModel>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AssigningAuthorityViewModel"/> class.
		/// </summary>
		/// <param name="assigningAuthority">The assigning authority.</param>
		public AssigningAuthorityViewModel(AssigningAuthorityInfo assigningAuthority)
		{
			AuthorityScopeList = assigningAuthority.AssigningAuthority.AuthorityScope.Select(x => new AuthorityScopeViewModel(x)).ToList();
			this.Id = assigningAuthority.Id;
			this.Name = assigningAuthority.AssigningAuthority.Name;
			this.Oid = assigningAuthority.AssigningAuthority.Oid;
			this.Url = assigningAuthority.AssigningAuthority.Url;
			this.DomainName = assigningAuthority.AssigningAuthority.DomainName;
			this.Description = assigningAuthority.AssigningAuthority.Description;
			this.ValidationRegex = assigningAuthority.AssigningAuthority.ValidationRegex;
			this.IsUnique = assigningAuthority.AssigningAuthority.IsUnique ? Locale.Yes : Locale.No;
		}

		/// <summary>
		/// Gets or sets the authority scopes.
		/// </summary>
		/// <value>The scopes assigned.</value>
		public List<AuthorityScopeViewModel> AuthorityScopeList { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		[Display(Name = "Description", ResourceType = typeof(Locale))]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the name of the domain.
		/// </summary>
		/// <value>The name of the domain.</value>
		[Display(Name = "DomainName", ResourceType = typeof(Locale))]
		public string DomainName { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is unique.
		/// </summary>
		/// <value><c>true</c> if this instance is unique; otherwise, <c>false</c>.</value>
		[Display(Name = "IsUnique", ResourceType = typeof(Locale))]
		public string IsUnique { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[Display(Name = "Name", ResourceType = typeof(Locale))]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the oid.
		/// </summary>
		/// <value>The oid.</value>
		[Display(Name = "Oid", ResourceType = typeof(Locale))]
		public string Oid { get; set; }

		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value>The URL.</value>
		[Display(Name = "Url", ResourceType = typeof(Locale))]
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets the regex.
		/// </summary>
		/// <value>The regex.</value>
		[Display(Name = "ValidationRegex", ResourceType = typeof(Locale))]
		public string ValidationRegex { get; set; }
	}
}