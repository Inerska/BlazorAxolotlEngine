// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using BlazorAxolotlEngine.Abstraction;
using BlazorAxolotlEngine.Abstraction.Component;
using BlazorAxolotlEngine.Abstraction.Entity;
using BlazorAxolotlEngine.Core;

namespace BlazorAxolotlEngine.Entity.Extension;

public static class AssignComponentSystemExtension
{
    public static bool AssignTo<TComponent>(this IWorld world, ISystem system, Type type)
        where TComponent : IComponentData
    {
        if (!typeof(IComponentData).IsAssignableFrom(type)) throw new ArgumentException("Type must be a component");

        var result = (world as World).TryGetGuid(system, out var guid);

        try
        {
            var components = world.Systems.TryGetValue(guid, out var value) ? value : new HashSet<Type>();
            components.Add(type);

            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    public static bool AssignTo(this IWorld world, ISystem system, Type componentType)
    {
        if (!typeof(IComponentData).IsAssignableFrom(componentType))
            throw new ArgumentException("The componentType must be a IComponentData");

        var result = (world as World).TryGetGuid(system, out var guid);

        try
        {
            var components = world.Systems.TryGetValue(guid, out var value)
                ? value
                : new HashSet<Type>();

            components.Add(componentType);

            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}