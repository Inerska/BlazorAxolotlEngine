// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using BlazorAxolotlEngine.Abstraction;
using BlazorAxolotlEngine.Abstraction.Entity;

namespace BlazorAxolotlEngine.Core;

public class World
    : IWorld
{
    public Dictionary<Guid, ISystem> Entities { get; } = new();

    public Guid SpawnEntity(ISystem system)
    {
        var guid = Guid.NewGuid();

        Entities.Add(guid, system);

        system.OnCreate(this);

        return guid;
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
}