using System;
using Blok3Game.Engine.GameObjects;
using Blok3Game.Engine.Helpers;
using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects;

public class World : GameObjectGrid
{
    // public const int CELL_SIZE = 2;
    // public const int CHUNKS_HORIZONTAL = 20;
    // public const int CHUNKS_VERTICAL = 20;
    //
    // public const int CELLS_PER_CHUNK_HORIZONTAL = 25;
    // public const int CELLS_PER_CHUNK_VERTICAL = 25;
    
    public const int CELL_SIZE = 10;
    public const int CHUNKS_HORIZONTAL = 10;
    public const int CHUNKS_VERTICAL = 10;
    
    public const int CELLS_PER_CHUNK_HORIZONTAL = 10;
    public const int CELLS_PER_CHUNK_VERTICAL = 10;

    public static World Instance { get; private set; }
    
    public World(int rows = CHUNKS_HORIZONTAL, int columns = CHUNKS_VERTICAL, int layer = 0, string id = "baseWorld") : base(rows, columns, layer, id)
    {
        if (Instance == null) { Instance = this; }
        else { throw new Exception("World already exists"); }
        
        CellWidth = CELL_SIZE * CELLS_PER_CHUNK_HORIZONTAL;
        CellHeight = CELL_SIZE * CELLS_PER_CHUNK_VERTICAL;
        CreateChunks();
    }

    private void CreateChunks()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Add(new Chunk(), j, i);
            }
        }
    }
    
    public override void Update(GameTime gameTime)
    {
        // Loop from the bottom right to the top left
        for (int x = Rows - 1; x >= 0; x--)
        {
            for (int y = Columns - 1; y >= 0; y--)
            {
                Get(x, y).Update(gameTime);
            }
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        
        if (inputHelper.MouseLeftButtonDown)
        {
            int x = (int)inputHelper.MousePosition.X / CELL_SIZE / CELLS_PER_CHUNK_HORIZONTAL;
            int y = (int)inputHelper.MousePosition.Y / CELL_SIZE / CELLS_PER_CHUNK_VERTICAL;
            
            if (x >= 0 && x < Columns && y >= 0 && y < Rows)
            {
                Chunk c = Get(x, y);
                c.Place(inputHelper.MousePosition, new Sand());
            }
        }
        
        if (inputHelper.MouseRightButtonDown)
        {
            int x = (int)inputHelper.MousePosition.X / CELL_SIZE / CELLS_PER_CHUNK_HORIZONTAL;
            int y = (int)inputHelper.MousePosition.Y / CELL_SIZE / CELLS_PER_CHUNK_VERTICAL;
            
            if (x >= 0 && x < Columns && y >= 0 && y < Rows)
            {
                Chunk c = Get(x, y);
                c.Place(inputHelper.MousePosition, new Water());
            }
        }
    }

    public override Chunk Get(int x, int y)
    {
        return base.Get(x, y) as Chunk;
    }
}