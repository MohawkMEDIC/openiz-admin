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
 * Date: 2017-2-19
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OpenIZ.Core.Model.Entities;
using OpenIZAdmin.Localization;
using OpenIZAdmin.Models.EntityIdentifierModels;

namespace OpenIZAdmin.Models.Core
{
	/// <summary>
	/// Represents an entity view model.
	/// </summary>
	public abstract class EntityViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EntityViewModel"/> class.
		/// </summary>
		protected EntityViewModel()
		{
			this.Identifiers = new List<EntityIdentifierViewModel>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EntityViewModel"/> class
		/// with a specific <see cref="Entity"/> instance.
		/// </summary>
		/// <param name="entity">The <see cref="Entity"/> instance.</param>
		protected EntityViewModel(Entity entity) : this()
		{
			this.CreationTime = entity.CreationTime.DateTime;
			this.Id = entity.Key.Value;
			this.Identifiers = entity.Identifiers.Select(i => new EntityIdentifierViewModel(i)).OrderBy(i => i.Name).ToList();
			this.IsObsolete = entity.ObsoletionTime.HasValue;
			this.Name = string.Join(" ", entity.Names.SelectMany(n => n.Component).Select(c => c.Value));
			this.ObsoletionTime = entity.ObsoletionTime?.DateTime;
			this.Type = entity.TypeConcept != null ? string.Join(" ", entity.TypeConcept.ConceptNames.Select(c => c.Name)) : "N/A";
		}

		/// <summary>
		/// Gets or sets the creation time of the entity.
		/// </summary>
		[Display(Name = "CreationTime", ResourceType = typeof(Locale))]
		public DateTime CreationTime { get; set; }

		/// <summary>
		/// Gets or sets the id of the entity.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets whether the entity is obsolete.
		/// </summary>
		public bool IsObsolete { get; set; }

		/// <summary>
		/// Gets or sets a list of identifiers associated with the entity.
		/// </summary>
		public List<EntityIdentifierViewModel> Identifiers { get; set; }

		/// <summary>
		/// Gets or sets the name of the entity.
		/// </summary>
		[Display(Name = "Name", ResourceType = typeof(Locale))]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the obsoletion time of the entity.
		/// </summary>
		[Display(Name = "ObsoletionTime", ResourceType = typeof(Locale))]
		public DateTime? ObsoletionTime { get; set; }

		/// <summary>
		/// Gets or sets the type of the entity.
		/// </summary>
		[Display(Name = "Type", ResourceType = typeof(Locale))]
		public string Type { get; set; }
	}
}