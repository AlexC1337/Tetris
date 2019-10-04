using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{
    /// The sprite of a single empty cell in the grid.
    // public Texture2D block, yellow, blue, green, babyblue, red, purple, orange;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;

    /// The number of grid elements in the x-direction.
    static public int Width { get { return 10; } }

    /// The number of grid elements in the y-direction.
    static public int Height { get { return 20; } }
    public Texture2D[,] array = new Texture2D[Width, Height];

  public Texture2D block = TetrisGame.ContentManager.Load<Texture2D>("block");
    public Texture2D yellow = TetrisGame.ContentManager.Load<Texture2D>("yellow");
    public Texture2D blue = TetrisGame.ContentManager.Load<Texture2D>("blue");
    public Texture2D red = TetrisGame.ContentManager.Load<Texture2D>("red");
    public Texture2D green = TetrisGame.ContentManager.Load<Texture2D>("green");
    public Texture2D purple = TetrisGame.ContentManager.Load<Texture2D>("purple");
    public Texture2D babyblue = TetrisGame.ContentManager.Load<Texture2D>("babyblue");
    public Texture2D orange = TetrisGame.ContentManager.Load<Texture2D>("orange");

    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        
        for (int y = 0; y != Height; y++)
        {
            for (int x = 0; x != Width; x++)
            {
                array[x, y] = block;
            }
        }
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
        Vector2 position = grid.position;
        for (int y = 0; y != Height; y++)
        {
            for (int x = 0; x != Width; x++)
            {
                if (array[x, y] == block)
                {
                    spriteBatch.Draw(grid.block, position, Color.White);   //als leeg teken lege cel
                }
                else
                {

                    spriteBatch.Draw(array[x, y], position, Color.White);   //als kleur, geef kleur

                }
                position.X += grid.block.Width;
            }
            position.X = 0;
            position.Y += grid.block.Height;
        }
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
    }
}
