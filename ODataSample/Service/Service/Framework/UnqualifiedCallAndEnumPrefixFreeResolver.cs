// ---------------------------------------------------------------------------
// <copyright file="UnqualifiedCallAndEnumPrefixFreeResolver.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

namespace ODataSample.Service.Framework
{
    using System;
    using System.Collections.Generic;

    using Microsoft.OData.Edm;
    using Microsoft.OData.UriParser;

    // Copied from WebApi\vNext\src\Microsoft.AspNet.OData\UnqualifiedCallAndEnumPrefixFreeResolver.cs
    // Until the day it becomes available in the nuget package
    public class UnqualifiedCallAndEnumPrefixFreeResolver : ODataUriResolver
    {
        private readonly StringAsEnumResolver stringAsEnum = new StringAsEnumResolver();

        private readonly UnqualifiedODataUriResolver unqualified = new UnqualifiedODataUriResolver();

        private bool enableCaseInsensitive;

        public override bool EnableCaseInsensitive
        {
            get
            {
                return this.enableCaseInsensitive;
            }
            set
            {
                this.enableCaseInsensitive = value;
                this.stringAsEnum.EnableCaseInsensitive = this.enableCaseInsensitive;
                this.unqualified.EnableCaseInsensitive = this.enableCaseInsensitive;
            }
        }

        public override IEnumerable<IEdmOperation> ResolveBoundOperations(IEdmModel model, string identifier, IEdmType bindingType)
        {
            return this.unqualified.ResolveBoundOperations(model, identifier, bindingType);
        }

        public override void PromoteBinaryOperandTypes(BinaryOperatorKind binaryOperatorKind, ref SingleValueNode leftNode, ref SingleValueNode rightNode, out IEdmTypeReference typeReference)
        {
            this.stringAsEnum.PromoteBinaryOperandTypes(binaryOperatorKind, ref leftNode, ref rightNode, out typeReference);
        }

        public override IEnumerable<KeyValuePair<string, object>> ResolveKeys(IEdmEntityType type, IDictionary<string, string> namedValues, Func<IEdmTypeReference, string, object> convertFunc)
        {
            return this.stringAsEnum.ResolveKeys(type, namedValues, convertFunc);
        }

        public override IEnumerable<KeyValuePair<string, object>> ResolveKeys(IEdmEntityType type, IList<string> positionalValues, Func<IEdmTypeReference, string, object> convertFunc)
        {
            return this.stringAsEnum.ResolveKeys(type, positionalValues, convertFunc);
        }

        public override IDictionary<IEdmOperationParameter, SingleValueNode> ResolveOperationParameters(IEdmOperation operation, IDictionary<string, SingleValueNode> input)
        {
            return this.stringAsEnum.ResolveOperationParameters(operation, input);
        }
    }
}