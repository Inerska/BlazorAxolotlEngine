// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using BlazorAxolotlEngine.Abstraction;
using BlazorAxolotlEngine.Abstraction.Component;
using BlazorAxolotlEngine.Abstraction.Entity;

namespace BlazorAxolotlEngine.Entity.Extension;

public static class AssignComponentSystemExtension
{
    public static bool AssignTo<TComponent>(this IWorld world, ISystem system, TComponent component)
        where TComponent : IComponentData
    {
        try
        {
            world.Components.Add(system, component);
        }
        catch (ArgumentException)
        {
            return false;
        }

        return true;
    }

    public static bool Assign<TComponent>(this ISystem system, IWorld world, TComponent component)
        where TComponent : IComponentData
    {
        try
        {
            world.Components.Add(system, component);
        }
        catch (ArgumentException)
        {
            return false;
        }

        return true;
    }
}