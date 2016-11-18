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

using OpenIZ.Core.Model.Security;
using OpenIZAdmin.Models.PolicyModels.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenIZAdmin.Models.DeviceModels
{
	public class EditDeviceModel
	{
		public EditDeviceModel()
		{
            this.Policies = new List<PolicyViewModel>();
        }

		[Display(Name = "Name", ResourceType = typeof(Localization.Locale))]
		[Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(Localization.Locale))]
		[StringLength(255, ErrorMessageResourceName = "NameTooLong", ErrorMessageResourceType = typeof(Localization.Locale))]
		public string Name { get; set; }        

        [Display(Name = "CreationTime", ResourceType = typeof(Localization.Locale))]
        public DateTime CreationTime { get; set; }

        public Guid Key { get; set; }

        public string Id { get; set; }

        public bool IsObsolete { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Localization.Locale))]
        public string Password { get; set; }

        public List<PolicyViewModel> Policies { get; set; }

        public List<SecurityPolicyInstance> DevicePolicies { get; set; }

        public DateTime? UpdatedTime { get; set; }
    }
}