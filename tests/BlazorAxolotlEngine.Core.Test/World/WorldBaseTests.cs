// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using BlazorAxolotlEngine.Abstraction;
using BlazorAxolotlEngine.Abstraction.Entity;
using BlazorAxolotlEngine.Component;
using BlazorAxolotlEngine.Entity.Extension;
using BlazorAxolotlEngine.Query.System.Extension;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace BlazorAxolotlEngine.Core.Test.World;

internal record TestSystem
    : ISystem
{
    public IWorld World { get; set; }

    public void OnCreate(IWorld world)
    {
        // Do nothing
    }

    public void OnDestroy()
    {
        // Do nothing
    }

    public void OnUpdate()
    {
        // Do nothing
    }
}

public class WorldBaseTests
{
    private readonly ITestOutputHelper _outputHelper;
    
    public WorldBaseTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    
    [Fact]
    public void SpawnEntity_Should_Add_To_Entity_Dictionary()
    {
        var world = new Core.World();
        var entity = new TestSystem();

        world.Should()
            .NotBeNull();

        _outputHelper.WriteLine($"Entity count: {world.Entities.Count}");
        
        var guid = world.SpawnEntity(entity);
        
        _outputHelper.WriteLine($"Entity GUID: {guid}");

        world.Entities.Should()
            .ContainKey(guid);
    }

    [Fact]
    public void Test()
    {
        var world = new Core.World();
        var entity = new TestSystem();

        world.Should().NotBeNull();
        entity.Should().NotBeNull();

        var _ = world.SpawnEntity(entity);

        world.AssignTo(entity, typeof(Transform));

        entity.Has<Transform>()
            .Should()
            .BeTrue();
    }
}