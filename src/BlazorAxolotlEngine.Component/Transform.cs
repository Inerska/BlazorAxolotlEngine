// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Numerics;
using BlazorAxolotlEngine.Abstraction.Component;

namespace BlazorAxolotlEngine.Component;

public struct Transform 
    : IComponentData
{
    Vector3 _position;
    Quaternion _rotation;
}