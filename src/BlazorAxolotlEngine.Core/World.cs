// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using BlazorAxolotlEngine.Abstraction;
using BlazorAxolotlEngine.Abstraction.Component;
using BlazorAxolotlEngine.Abstraction.Entity;

namespace BlazorAxolotlEngine.Core;

public class World
    : IWorld
{
    public Dictionary<Guid, ISystem> Entities { get; } = new();

    public Dictionary<Guid, HashSet<IComponentData>> Systems { get; set; } = new();
    public Dictionary<Type, IComponentData> Components { get; set; } = new();

    public Guid SpawnEntity(ISystem system)
    {
        var guid = Guid.NewGuid();

        Entities.Add(guid, system);
        Systems.Add(guid, new HashSet<IComponentData>());

        system.World = this;
        system.OnCreate(this);

        return guid;
    }

    public bool TryGetGuid(ISystem system, out Guid guid)
    {
        foreach (var (key, value) in Entities)
        {
            if (value != system) continue;
            
            guid = key;
            return true;
        }

        guid = Guid.Empty;
        return false;
    }

    public void DestroyEntity(ISystem system)
    {
        var guid = Entities.FirstOrDefault(x => x.Value == system).Key;

        Entities.Remove(guid);

        system.OnDestroy();
    }

    public void Update()
    {
        Entities.Values.ToList().ForEach(x => x.OnUpdate());
    }

    public Guid[] SpawnEntities(params ISystem[] systems)
    {
        return systems.Select(SpawnEntity).ToArray();
    }
}