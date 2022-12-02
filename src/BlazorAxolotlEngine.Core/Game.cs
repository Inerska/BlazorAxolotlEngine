// Copyright (c) Alexis Chân Gridel. All Rights Reserved.
// Licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Blazor.Extensions;
using Blazor.Extensions.Canvas.WebGL;
using BlazorAxolotlEngine.Abstraction;

namespace BlazorAxolotlEngine.Core;

public class Game
    : IGame
{
    public BECanvasComponent Canvas { get; set; }
    private readonly IWorld _world;
    private WebGLContext _context;

    public Game()
    {
        _world = new World();
        Canvas = new BECanvasComponent();
    }

    public async Task RunAsync()
    {
        _context = await Canvas.CreateWebGLAsync();
    }
}