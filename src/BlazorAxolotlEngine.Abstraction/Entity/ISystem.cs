// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

namespace BlazorAxolotlEngine.Abstraction.Entity;

/// <summary>
///     Represents a single entity in the game.
///     You need to derive from this class to create your own entities.
/// </summary>
public interface ISystem
{
    public IWorld World { get; set; }

    public void OnCreate(IWorld world);
    public void OnDestroy();
    public void OnUpdate();
}