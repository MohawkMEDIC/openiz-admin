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
 * Date: 2016-7-11
 */
using OpenIZAdmin.Models.CertificateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace OpenIZAdmin.Models.Ami
{
	/// <summary>
	/// Submission request
	/// </summary>
	[XmlType(nameof(SubmissionRequest), Namespace = "http://openiz.org/ami")]
	[XmlRoot(nameof(SubmissionRequest), Namespace = "http://openiz.org/ami")]
	public class SubmissionRequest
	{
		public SubmissionRequest()
		{

		}

		public SubmissionRequest(SubmitCertificateSigningRequestModel model)
		{
			this.CmcRequest = model.CmcRequest;
			this.AdminAddress = model.AdministrativeContactEmail;
			this.AdminContactName = model.AdministrativeContactName;
		}

		/// <summary>
		/// Gets or sets the cmc request
		/// </summary>
		[XmlElement("cmc")]
		public String CmcRequest { get; set; }

		/// <summary>
		/// Gets or sets the contact name
		/// </summary>
		[XmlElement("contact")]
		public String AdminContactName { get; set; }

		/// <summary>
		/// Gets or sets the admin address
		/// </summary>
		[XmlElement("address")]
		public String AdminAddress { get; set; }
	}
}