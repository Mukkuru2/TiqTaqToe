using System;
using Blok3Game.Engine.GameObjects;
using Blok3Game.Engine.Helpers;
using Blok3Game.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects;

public class Chunk : GameObjectGrid
{
    public bool IsActive { get; private set; }
    private int timeNoUpdates = 0;
    private int maxChunkLifetime = 3;


    public Chunk(int rows = World.CELLS_PER_CHUNK_HORIZONTAL, int columns = World.CELLS_PER_CHUNK_VERTICAL, int layer = 0, string id = "baseWorld") : base(rows, columns, layer, id)
    {
        CellWidth = World.CELL_SIZE;
        CellHeight = World.CELL_SIZE;
        FillAir();
    }

    private void FillAir()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Add (new Air(), j, i);
            }
        }
    }
    
    public override void Update(GameTime gameTime)
    {
        if (timeNoUpdates > maxChunkLifetime)
        {
            IsActive = false;
            return;
        }
        
        // Loop from the bottom right to the top left
        for (int i = Rows - 1; i >= 0; i--)
        {
            for (int j = Columns - 1; j >= 0; j--)
            {
                Get(i, j).Update(gameTime);
            }
        }
        
        // Resets to 0 if the chunk has been updated
        timeNoUpdates++;
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (GameState.DEBUG_LEVEL >= GameState.DebugLevel.INFO)
        {
            Color c = IsActive ? Color.Green : Color.Red;
            DrawingHelper.DrawRectangle(
                new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, CellWidth * Columns - 2,
                    CellHeight * Rows - 2), spriteBatch, c, 2);
        }

        // Draw all the individual cells
        for (int i = Rows - 1; i >= 0; i--)
        {
            for (int j = Columns - 1; j >= 0; j--)
            {
                Get(i, j).Draw(gameTime, spriteBatch);
            }
        }
    }

    public void Place(Vector2 position, Element element, bool full = true)
    {
        SetActive();
        
        // Check each cell in the chunk
        for (int x = Rows - 1; x >= 0; x--)
        {
            for (int y = Columns - 1; y >= 0; y--)
            {
                // If the cell is empty, place the element
                if (Get(x, y) is not Air) continue;
                
                // If the cell intersects with the position, place the element
                if (new Rectangle((int)GlobalPosition.X + x * CellWidth, (int)GlobalPosition.Y + y * CellHeight, CellWidth, CellHeight).Contains(position))
                {
                    Add(element, x, y);
                    return;
                }
            }
        }
    }
    
    public void Move(Element elem, int x, int y)
    {
        SetActive();
        
        int oldIndexX = elem.X;
        int oldIndexY = elem.Y;
        
        Element oldElem = Get(x, y);
        
        Vector2 index = World.Instance.GetIndex(this);
        
        // If the element move within the chunk, apply the move normally
        if (x >= 0 && x < Columns && y >= 0 && y < Rows)
        {
            Set(x, y, elem);
            elem.Position = new Vector2(x * CellWidth, y * CellHeight);
            
            Set(oldIndexX, oldIndexY, oldElem);
            oldElem.Position = new Vector2(oldIndexX * CellWidth, oldIndexY * CellHeight);
            return;
        }
        
        // If the chunk is at the bottom of the world and the element moves down, do nothing
        if (y >= Rows && (int)index.Y == World.Instance.Rows - 1) return;
        
        // If there is a chunk below, get the chunk.
        int dx = x < 0 ? -1 : x >= Columns ? 1 : 0;
        int dy = y < 0 ? -1 : y >= Rows ? 1 : 0;

        int _x = x - dx * Columns;
        int _y = y - dy * Rows;
        
        Chunk _c = World.Instance.Get((int)index.X + dx, (int)index.Y + dy);
        
        SetActive(_c);
        
        _c.Set(_x, _y, elem);
        elem.Position = new Vector2(_x * CellWidth, _y * CellHeight);
        elem.Parent = _c;
        
        Set(oldIndexX, oldIndexY, oldElem);
        oldElem.Position = new Vector2(oldIndexX * CellWidth, oldIndexY * CellHeight);
        oldElem.Parent = this;
    }

    public override Element Get(int x, int y)
    {
        if (x >= 0 && x < Columns && y >= 0 && y < Rows)
        {
            return base.Get(x, y) as Element;
        }
                
        // Set x to negative 1 if < 0 and to 1 if x > columns
        int dx = x < 0 ? -1 : x >= Columns ? 1 : 0;
        int dy = y < 0 ? -1 : y >= Rows ? 1 : 0;
        
        Chunk _c = GetChunkRelative(x, y, dx, dy);
        return _c?.Get(x - dx * Columns, y - dy * Rows);
    }

    /// <summary>
    /// Gets the chunk that belongs to the element at a given position.
    /// If the element is one lower than the lowest element in this chunk it returns the chunk below.
    /// Only works with range 1 relative to this chunk (diagonals included).
    /// </summary>
    /// <returns></returns>
    public Chunk GetChunkRelative(int x, int y, int dx, int dy)
    {
        Vector2 index = World.Instance.GetIndex(this);
        
        // Check if there are chunks to dx and dy of the chunks. If not, return null
        if ((int)index.X + dx < 0 || (int)index.X + dx >= World.Instance.Columns || (int)index.Y + dy < 0 || (int)index.Y + dy >= World.Instance.Rows)
        {
            return null;
        }
        
        return World.Instance.Get((int)index.X + dx, (int)index.Y + dy);
    }

    public void SetActive()
    {
        IsActive = true;
        timeNoUpdates = 0;
    }
    
    public void SetActive(Chunk c)
    {
        c.IsActive = true;
        c.timeNoUpdates = 0;
    }
}