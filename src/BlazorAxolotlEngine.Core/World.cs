// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using BlazorAxolotlEngine.Abstraction;
using BlazorAxolotlEngine.Abstraction.Entity;

namespace BlazorAxolotlEngine.Core;

public class World
    : IWorld
{
    public Dictionary<Guid, IEntity> Entities { get; } = new();

    public Guid SpawnEntity(IEntity entity)
    {
        var guid = Guid.NewGuid();

        Entities.Add(guid, entity);

        entity.OnCreate(this);

        return guid;
    }

    public void DestroyEntity(IEntity entity)
    {
        var guid = Entities.FirstOrDefault(x => x.Value == entity).Key;

        Entities.Remove(guid);

        entity.OnDestroy();
    }

    public void Update()
    {
        Entities.Values.ToList().ForEach(x => x.OnUpdate());
    }
}