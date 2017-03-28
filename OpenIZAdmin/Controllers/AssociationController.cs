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
 * User: khannan
 * Date: 2017-3-27
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using OpenIZ.Core.Model.Entities;
using OpenIZAdmin.Attributes;

namespace OpenIZAdmin.Controllers
{
	/// <summary>
	/// Provides operations for managing associations.
	/// </summary>
	/// <seealso cref="OpenIZAdmin.Controllers.BaseController" />
	[TokenAuthorize]
	public abstract class AssociationController : BaseController
	{
		/// <summary>
		/// Gets the entity.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="modelType">Type of the model.</param>
		/// <returns>Entity.</returns>
		protected virtual Entity GetEntity(Guid id, Type modelType)
		{
			var getMethod = this.ImsiClient.GetType().GetRuntimeMethod("Get", new Type[] { typeof(Guid), typeof(Guid?) }).MakeGenericMethod(modelType);

			return getMethod.Invoke(this.ImsiClient, new object[] { id, null }) as Entity;
		}

		/// <summary>
		/// Gets the type of the model.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>Returns the type for a given model type.</returns>
		/// <exception cref="System.ArgumentException">If the model type is not supported.</exception>
		protected virtual Type GetModelType(string type)
		{
			Type modelType;
			switch (type)
			{
				case "Material":
					modelType = typeof(Material);
					break;
				case "Place":
					modelType = typeof(Place);
					break;
				case "Organization":
					modelType = typeof(Organization);
					break;
				default:
					throw new ArgumentException($"Unsupported type: { type }");
			}

			return modelType;
		}

		/// <summary>
		/// Updates the entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="modelType">Type of the model.</param>
		/// <returns>Entity.</returns>
		protected virtual Entity UpdateEntity(Entity entity, Type modelType)
		{
			var updateMethod = this.ImsiClient.GetType().GetRuntimeMethods().First(m => m.Name == "Update" && m.IsGenericMethod).MakeGenericMethod(modelType);

			return updateMethod.Invoke(this.ImsiClient, new object[] { entity }) as Entity;
		}
	}
}