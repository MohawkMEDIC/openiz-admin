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
 * Date: 2017-4-12
 */

using OpenIZ.Core.Model.DataTypes;
using OpenIZAdmin.Localization;
using OpenIZAdmin.Models.ConceptModels;
using OpenIZAdmin.Models.ReferenceTermNameModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OpenIZAdmin.Models.ReferenceTermModels
{
	/// <summary>
	/// Represents a model to view a reference term.
	/// </summary>
	public class ReferenceTermViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReferenceTermViewModel"/> class.
		/// </summary>
		public ReferenceTermViewModel()
		{
			this.Concepts = new List<ConceptViewModel>();
			this.DisplayNames = new List<ReferenceTermName>();
			this.ReferenceTermNames = new List<ReferenceTermNameViewModel>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReferenceTermViewModel"/> class.
		/// </summary>
		public ReferenceTermViewModel(ReferenceTerm referenceTerm) : this()
		{
			this.CreationTime = referenceTerm.CreationTime.DateTime;
			this.CodeSystem = referenceTerm.CodeSystem?.Oid;
			this.DisplayNames = referenceTerm.DisplayNames;
			this.Id = referenceTerm.Key.Value;
			this.Mnemonic = referenceTerm.Mnemonic;
			this.Names = string.Join(", ", referenceTerm.DisplayNames.Select(d => d.Name));
			this.ReferenceTermNames = referenceTerm.DisplayNames.Select(n => new ReferenceTermNameViewModel(n)).ToList();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReferenceTermViewModel"/> class.
		/// </summary>
		public ReferenceTermViewModel(ReferenceTerm term, Concept concept) : this(term)
		{
			ConceptId = concept?.Key;
			ConceptVersionKey = concept?.VersionKey;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReferenceTermViewModel"/> class.
		/// </summary>
		public ReferenceTermViewModel(Concept concept)
		{
			ConceptId = concept?.Key;
			ConceptVersionKey = concept?.VersionKey;
		}

		/// <summary>
		/// Gets or sets the code system.
		/// </summary>
		/// <value>The code system.</value>
		[Display(Name = "CodeSystem", ResourceType = typeof(Locale))]
		public string CodeSystem { get; set; }

		/// <summary>
		/// Gets or sets the concept identifier associated with the reference term.
		/// </summary>
		public Guid? ConceptId { get; set; }

		/// <summary>
		/// Gets or sets the concepts.
		/// </summary>
		/// <value>The concepts.</value>
		public List<ConceptViewModel> Concepts { get; set; }

		/// <summary>
		///  Gets or sets the concept version identifier associated with the reference term
		/// </summary>
		public Guid? ConceptVersionKey { get; set; }

		/// <summary>
		/// Gets or sets the creation time of the concept.
		/// </summary>
		[Display(Name = "CreationTime", ResourceType = typeof(Locale))]
		public DateTime CreationTime { get; set; }

		/// <summary>
		/// Gets or sets the display names.
		/// </summary>
		/// <value>The display names.</value>
		public List<ReferenceTermName> DisplayNames { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[Required]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the mnemonic.
		/// </summary>
		/// <value>The mnemonic.</value>
		[Display(Name = "Mnemonic", ResourceType = typeof(Locale))]
		public string Mnemonic { get; set; }

		/// <summary>
		/// Gets or sets the concatenated display names of the Reference Term
		/// </summary>
		[Display(Name = "Names", ResourceType = typeof(Locale))]
		public string Names { get; set; }

		/// <summary>
		/// Gets or sets the list of reference names associated with the reference term
		/// </summary>
		public List<ReferenceTermNameViewModel> ReferenceTermNames { get; set; }
	}
}