// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using BlazorAxolotlEngine.Abstraction;
using BlazorAxolotlEngine.Abstraction.Entity;

namespace BlazorAxolotlEngine.Core;

public class World
    : IWorld
{
    private readonly Dictionary<Guid, IEntity> _entities = new();

    public void SpawnEntity(IEntity entity)
    {
        var guid = Guid.NewGuid();

        _entities.Add(guid, entity);

        entity.OnCreate(this);
    }

    public void DestroyEntity(IEntity entity)
    {
        var guid = _entities.FirstOrDefault(x => x.Value == entity).Key;

        _entities.Remove(guid);

        entity.OnDestroy();
    }

    public void Update()
    {
        _entities.Values.ToList().ForEach(x => x.OnUpdate());
    }
}