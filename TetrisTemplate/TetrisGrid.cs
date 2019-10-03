using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;
    Texture2D filledCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;

    /// The number of grid elements in the x-direction.
    static public int Width { get { return 10; } }

    /// The number of grid elements in the y-direction.
    static public int Height { get { return 20; } }
    public int[,] array = new int[Width, Height];

    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        filledCell = TetrisGame.ContentManager.Load<Texture2D>("filledblock");
        position = Vector2.Zero;
        Clear();
    }

    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        TetrisGrid grid = new TetrisGrid();
        int height = TetrisGrid.Height;
        int width = TetrisGrid.Width;
        Vector2 position = grid.position;
        int[,] array = new int[width, height];
        for (int y = 0; y != height; y++)
        {
            for (int x = 0; x != width; x++)
            {
                if (array[x, y] == 0)
                {
                    spriteBatch.Draw(grid.emptyCell, position, Color.White);
                }
                if (array[x, y] == 1)
                {
                    spriteBatch.Draw(grid.filledCell, position, Color.White);
                }
                position.X += grid.emptyCell.Width;
            }
            position.X = 0;
            position.Y += grid.emptyCell.Height;
        }
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
    }
}

