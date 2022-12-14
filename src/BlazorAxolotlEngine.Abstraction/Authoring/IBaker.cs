// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

namespace BlazorAxolotlEngine.Abstraction.Authoring;

public interface IBaker<in TAuthoring>
{
    public void Bake(TAuthoring authoring);
}