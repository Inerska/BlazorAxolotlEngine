// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using BlazorAxolotlEngine.Abstraction.Component;
using BlazorAxolotlEngine.Abstraction.Entity;
using BlazorAxolotlEngine.Core;

namespace BlazorAxolotlEngine.Query.System.Extension;

public static class HasComponentSystemExtension
{
    public static bool Has<TComponent>(this ISystem system)
        where TComponent : IComponentData
    {
        var world = system.World as World;

        var result = world.TryGetGuid(system, out var guid);

        if (!world.Systems.ContainsKey(guid)) return false;

        var components = world.Systems.TryGetValue(guid, out var value) ? value : new HashSet<IComponentData>();
        
        return components.Any(x => x is TComponent);
    }
}