// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using BlazorAxolotlEngine.Abstraction;
using BlazorAxolotlEngine.Abstraction.Component;
using BlazorAxolotlEngine.Abstraction.Entity;
using BlazorAxolotlEngine.Core;
using BlazorAxolotlEngine.Core.Exception;
using BlazorAxolotlEngine.Core.Test.World;

namespace BlazorAxolotlEngine.Entity.Extension;

public static class AssignComponentSystemExtension
{
    private static bool AssignTo(this IWorld world, ISystem system, Type componentType)
    {
        if (!typeof(IComponentData).IsAssignableFrom(componentType))
            throw new ArgumentException("The componentType must be a IComponentData");
        
        var componentData = (IComponentData)Activator.CreateInstance(componentType);

        var result = (world as World).TryGetGuid(system, out var guid);

        if (!result) throw new NotPresentInWorldException();

        if (world.Systems[guid].Contains(componentData)) throw new AlreadyHasComponentException();

        try
        {
            var components = world.Systems.TryGetValue(guid, out var value)
                ? value
                : new HashSet<IComponentData>();

            components.Add(componentData);

            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    public static bool AssignTo<TComponent>(this IWorld world, ISystem system)
        where TComponent : IComponentData
    {
        return world.AssignTo(system, typeof(TComponent));
    }

    public static bool Assign<TComponent>(this ISystem system)
    {
        return system.World.AssignTo(system, typeof(TComponent));
    }
}