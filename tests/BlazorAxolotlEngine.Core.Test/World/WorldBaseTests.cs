// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using BlazorAxolotlEngine.Abstraction;
using BlazorAxolotlEngine.Abstraction.Entity;
using FluentAssertions;
using Xunit;

namespace BlazorAxolotlEngine.Core.Test.World;

internal record TestEntity : IEntity
{
    public void OnCreate(IWorld world)
    {
    }

    public void OnDestroy()
    {
    }

    public void OnUpdate()
    {
    }
}

public class WorldBaseTests
{
    [Fact]
    public void SpawnEntity_Should_Add_To_Entity_Dictionary()
    {
        var world = new Core.World();
        var entity = new TestEntity();

        world.Should()
            .NotBeNull();
        
        var guid = world.SpawnEntity(entity);
        
        world.Entities.Should()
            .ContainKey(guid);
    }
}