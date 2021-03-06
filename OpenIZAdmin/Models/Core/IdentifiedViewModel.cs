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
 * Date: 2017-4-17
 */

using OpenIZ.Core.Model;
using OpenIZAdmin.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace OpenIZAdmin.Models.Core
{
	/// <summary>
	/// Represents an identified view model.
	/// </summary>
	/// <seealso cref="OpenIZAdmin.Models.Core.IIdentifiable" />
	public abstract class IdentifiedViewModel : IIdentifiable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IdentifiedViewModel"/> class.
		/// </summary>
		protected IdentifiedViewModel()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentifiedViewModel"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		protected IdentifiedViewModel(Guid id)
		{
			this.Id = id;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentifiedViewModel"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="creationTime">The creation time.</param>
		protected IdentifiedViewModel(Guid id, DateTimeOffset creationTime) : this(id)
		{
			this.CreationTime = creationTime.DateTime;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentifiedViewModel"/> class.
		/// </summary>
		/// <param name="baseEntityData">The base entity data.</param>
		protected IdentifiedViewModel(BaseEntityData baseEntityData) : this(baseEntityData.Key.Value, baseEntityData.CreationTime)
		{
		}

		/// <summary>
		/// Gets or sets the creation time of the entity.
		/// </summary>
		[Display(Name = "CreationTime", ResourceType = typeof(Locale))]
		public DateTime CreationTime { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public Guid Id { get; set; }
	}
}