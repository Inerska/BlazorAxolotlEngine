// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using BlazorAxolotlEngine.Abstraction.Component;

namespace BlazorAxolotlEngine.Abstraction;

/// <summary>
///     Represents a place where systems can be registered and executed.
/// </summary>
public interface IWorld
{
    public Dictionary<Guid, HashSet<IComponentData>> Systems { get; set; }
}