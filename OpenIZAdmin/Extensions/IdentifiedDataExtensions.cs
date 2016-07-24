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
 * User: Nityan
 * Date: 2016-7-23
 */

using OpenIZ.Core.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenIZAdmin.Extensions
{
	public static class IdentifiedDataExtensions
	{
		public static List<EntityName> AddNames(this Entity source, Guid nameUse, Guid componentType, List<string> names)
		{
			source.Names = new List<EntityName>();

			source.Names.AddRange(names.Select(n => new EntityName
			{
				Component = new List<EntityNameComponent>
				{
					new EntityNameComponent(componentType, n)
				}
			}));

			return source.Names;
		}
	}
}