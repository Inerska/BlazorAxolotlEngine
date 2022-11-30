// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using BlazorAxolotlEngine.Abstraction.Component;
using BlazorAxolotlEngine.Abstraction.Entity;
using BlazorAxolotlEngine.Core.Exception;

namespace BlazorAxolotlEngine.Core.Extension;

public static class GetComponentSystemExtension
{
    public static bool TryGetComponent<TComponent>(this ISystem system, out TComponent component)
        where TComponent : IComponentData
    {
        var world = system.World as World;

        if (!world.TryGetGuid(system, out var guid)) throw new NotPresentInWorldException();

        component = world.Systems[guid].OfType<TComponent>().FirstOrDefault();
        return component != null;
    }

    public static TComponent Get<TComponent>(this ISystem system)
        where TComponent : IComponentData
    {
        var world = system.World as World;

        if (!world.TryGetGuid(system, out var guid)) throw new NotPresentInWorldException();

        try
        {
            return world.Systems[guid].OfType<TComponent>().First();
        }
        catch (System.Exception)
        {
            throw new NoComponentException();
        }
    }
}