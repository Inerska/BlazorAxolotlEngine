// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Numerics;
using BlazorAxolotlEngine.Abstraction;
using BlazorAxolotlEngine.Abstraction.Component;
using BlazorAxolotlEngine.Abstraction.Entity;
using BlazorAxolotlEngine.Component;
using BlazorAxolotlEngine.Core.Exception;
using BlazorAxolotlEngine.Core.Extension;
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
    public void SpawnedEntity_Should_Not_Have_Any_Component()
    {
        var world = new Core.World();
        var entity = new TestSystem();

        world.SpawnEntity(entity);

        entity.Has<TransformComponent>()
            .Should()
            .BeFalse();
    }

    [Fact]
    public void SpawnedEntity_Should_Have_A_Component_When_Added()
    {
        var world = new Core.World();
        var entity = new TestSystem();

        world.SpawnEntity(entity);

        world.AssignTo<TransformComponent>(entity);

        entity.Has<TransformComponent>()
            .Should()
            .BeTrue();
    }

    [Fact]
    public void SpawnedEntity_Should_Have_A_Component_When_Added_With_Component_Even_If_There_Are_Multiple_Entities()
    {
        var world = new Core.World();
        var entity1 = new TestSystem();
        var entity2 = new TestSystem();

        world.SpawnEntity(entity1);
        world.SpawnEntity(entity2);

        world.AssignTo<TransformComponent>(entity1);

        entity1.Has<TransformComponent>()
            .Should()
            .BeTrue();

        entity2.Has<TransformComponent>()
            .Should()
            .BeFalse();
    }

    [Fact]
    public void AssignTo_T_To_ISystem_Should_Add_Component()
    {
        var world = new Core.World();
        var entity = new TestSystem();

        world.SpawnEntity(entity);

        world.AssignTo<TransformComponent>(entity);

        entity.Has<TransformComponent>()
            .Should()
            .BeTrue();
    }

    [Fact]
    public void AssignTo_T_To_ISystem_Not_In_World_Should_Throw_NotPresentInWorldException()
    {
        var world = new Core.World();
        var entity = new TestSystem();

        world.Invoking(w => w.AssignTo<TransformComponent>(entity))
            .Should()
            .Throw<NotPresentInWorldException>();
    }

    [Fact]
    public void AssignTo_T_To_ISystem_With_Component_Should_Throw_AlreadyHasComponentException()
    {
        var world = new Core.World();
        var entity = new TestSystem();

        world.SpawnEntity(entity);

        world.AssignTo<TransformComponent>(entity);

        world.Invoking(w => w.AssignTo<TransformComponent>(entity))
            .Should()
            .Throw<AlreadyHasComponentException>();
    }

    [Fact]
    public void TryGetComponent_T_Of_ISystem_Which_Has_Component_Should_Return_True_And_Component()
    {
        var world = new Core.World();
        var entity = new TestSystem();

        world.SpawnEntity(entity);

        world.AssignTo<TransformComponent>(entity);

        entity.TryGetComponent<TransformComponent>(out var component)
            .Should()
            .BeTrue();

        component.Should()
            .NotBeNull()
            .And
            .BeOfType<TransformComponent>();
    }
    
    [Fact]
    public void Get_Component_Of_ISystem_Which_Has_Component_Should_Return_Component()
    {
        var world = new Core.World();
        var entity = new TestSystem();

        world.SpawnEntity(entity);

        world.AssignTo<TransformComponent>(entity);

        var component = entity.Get<TransformComponent>();

        component.Should()
            .NotBeNull()
            .And
            .BeOfType<TransformComponent>();
    }
    
    [Fact]
    public void Get_Component_Of_ISystem_Which_Has_No_Component_Should_Throw_NoComponentException()
    {
        var world = new Core.World();
        var entity = new TestSystem();

        world.SpawnEntity(entity);

        entity.Invoking(e => e.Get<TransformComponent>())
            .Should()
            .Throw<NoComponentException>();
    }
    
    [Fact]
    public void Get_Component_And_Modify_It_Should_Modify_The_Component()
    {
        var world = new Core.World();
        var entity = new TestSystem();

        world.SpawnEntity(entity);

        world.AssignTo<TransformComponent>(entity);

        var component = entity.Get<TransformComponent>();

        component.Position = new Vector3(1, 2, 3);
        
        component.Position.Should()
            .Be(new Vector3(1, 2, 3));
    }
}