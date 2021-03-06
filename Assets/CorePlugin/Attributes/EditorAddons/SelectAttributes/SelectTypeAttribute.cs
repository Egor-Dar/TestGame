#region license

// Copyright 2021 - 2021 Arcueid Elizabeth D'athemon
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//     http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Diagnostics;
using CorePlugin.Extensions;

namespace CorePlugin.Attributes.EditorAddons.SelectAttributes
{
    /// <summary>
    /// Attribute for Type selection in Inspector.
    /// Use in pair with [SerializeReference] Attribute.
    /// </summary>
    [Conditional(EditorDefinition.UnityEditor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class SelectTypeAttribute : SelectAttributeBase
    {
        public SelectTypeAttribute(Type type) : base(type)
        {
        }
    }
}
