// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using BlazorAxolotlEngine.Abstraction.Component;
using BlazorAxolotlEngine.Abstraction.Entity;

namespace BlazorAxolotlEngine.Abstraction;

public interface IWorld
{
    public Dictionary<ISystem, IComponentData> Components { get; }
}