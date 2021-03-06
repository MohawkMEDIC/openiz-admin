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
 * Date: 2017-4-14
 */

using OpenIZ.Core.Model;
using OpenIZ.Core.Model.AMI.Security;
using OpenIZ.Core.Model.DataTypes;
using OpenIZAdmin.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenIZAdmin.Controllers
{
	/// <summary>
	/// Provides operations for managing metadata.
	/// </summary>
	/// <seealso cref="OpenIZAdmin.Controllers.BaseController" />
	[TokenAuthorize]
	public class MetadataController : BaseController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MetadataController"/> class.
		/// </summary>
		public MetadataController()
		{
		}

		/// <summary>
		/// Checks if a concept exists.
		/// </summary>
		/// <param name="mnemonic">The mnemonic.</param>
		/// <returns><c>true</c> if the concept exists, <c>false</c> otherwise.</returns>
		protected virtual bool DoesConceptExist(string mnemonic)
		{
			return this.GetConcept(mnemonic) != null;
		}

		/// <summary>
		/// Checks if a concept exists.
		/// </summary>
		/// <param name="mnemonic">The mnemonic.</param>
		/// <returns><c>true</c> if the concept set exists, <c>false</c> otherwise.</returns>
		protected virtual bool DoesConceptSetExist(string mnemonic)
		{
			return this.GetConceptSet(mnemonic) != null;
		}

		/// <summary>
		/// Gets the code systems.
		/// </summary>
		/// <returns>Returns a list of concept classes.</returns>
		protected AmiCollection<CodeSystem> GetCodeSystems()
		{
			return this.AmiClient.GetCodeSystems(c => c.ObsoletionTime == null);
		}

		/// <summary>
		/// Gets the concept class.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>Returns the concept class for the given key, or null if no concept class is found.</returns>
		protected ConceptClass GetConceptClass(Guid key)
		{
			return this.ImsiClient.Get<ConceptClass>(key, null) as ConceptClass;
		}

		/// <summary>
		/// Gets the concept classes.
		/// </summary>
		/// <returns>Returns a list of concept classes.</returns>
		protected IEnumerable<ConceptClass> GetConceptClasses()
		{
			var bundle = this.ImsiClient.Query<ConceptClass>(c => c.ObsoletionTime == null, 0, null, true);

			bundle.Reconstitute();

			return bundle.Item.OfType<ConceptClass>().Where(c => c.ObsoletionTime == null);
		}

		/// <summary>
		/// Gets the concept reference terms.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="versionId">The version identifier.</param>
		/// <returns>Returns a list of reference terms for a given concept.</returns>
		protected IEnumerable<ReferenceTerm> GetConceptReferenceTerms(Guid id, Guid? versionId)
		{
			var referenceTerms = new List<ReferenceTerm>();

			var bundle = this.ImsiClient.Query<Concept>(c => c.Key == id && c.VersionKey == versionId && c.ObsoletionTime == null);

			bundle.Reconstitute();

			foreach (var conceptReferenceTerm in bundle.Item.OfType<Concept>().LatestVersionOnly().Where(c => c.Key == id && c.VersionKey == versionId && c.ObsoletionTime == null).SelectMany(c => c.ReferenceTerms))
			{
				var referenceTerm = conceptReferenceTerm.ReferenceTerm;

				if (referenceTerm == null && conceptReferenceTerm.ReferenceTermKey.HasValue && conceptReferenceTerm.ReferenceTermKey.Value != Guid.Empty)
				{
					var referenceTermBundle = this.ImsiClient.Query<ReferenceTerm>(c => c.Key == conceptReferenceTerm.ReferenceTermKey && c.ObsoletionTime == null, 0, null, true);

					referenceTermBundle.Reconstitute();

					referenceTerm = referenceTermBundle.Item.OfType<ReferenceTerm>().FirstOrDefault(c => c.Key == conceptReferenceTerm.ReferenceTermKey && c.ObsoletionTime == null);
				}

				if (referenceTerm != null)
				{
					referenceTerms.Add(referenceTerm);
				}
			}

			return referenceTerms;
		}

		/// <summary>
		/// Loads the concept sets.
		/// </summary>
		/// <param name="conceptKey">The concept key.</param>
		/// <returns>Returns a list of <see cref="Guid"/> values which represents the concept sets which are associated with the given concept.</returns>
		protected List<Guid> LoadConceptSets(Guid conceptKey)
		{
			// ensure existing concept sets are sent up otherwise
			// the IMS will remove this concept from any associated concept set
			var bundle = this.ImsiClient.Query<ConceptSet>(cs => cs.ConceptsXml.Any(c => c == conceptKey) && cs.ObsoletionTime == null, 0, null, true);

			bundle.Reconstitute();

			return bundle.Item.OfType<ConceptSet>().Where(cs => cs.ConceptsXml.Any(c => c == conceptKey) && cs.Key.HasValue && cs.ObsoletionTime == null).Select(c => c.Key.Value).ToList();
		}
	}
}