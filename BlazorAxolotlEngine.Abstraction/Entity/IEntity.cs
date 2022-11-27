// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

namespace BlazorAxolotlEngine.Abstraction;

public interface IEntity
{
    public void OnCreate();
    public void OnDestroy();
    public void OnUpdate();
}