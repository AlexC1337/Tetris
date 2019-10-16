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
    public Vector2 position;

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
        Vector2 drawPos = position;
        for (int y = 0; y != Height; y++)
        {
            for (int x = 0; x != Width; x++)
            {
                spriteBatch.Draw(array[x, y], drawPos, Color.White);   //als leeg teken lege cel             
                drawPos.X += block.Width;
            }
            drawPos.X = 0;
            drawPos.Y += block.Height;
        }
    }

    public void CheckfullLine()
    {
        for (int i= Height- 1; i > 0; i--)
        {
            if (FullLine(i))
            {
                ClearLine(i);
              //  MoveDown(Height);
            }
        }
    }

    bool FullLine(int i)
    {
        bool check = true;
        for (int j = 0; j< Width-1; j++)
        {
            if (array[j, i] == block)
            check = false;
        }
        return check;
    }
        
   void ClearLine(int i)
    {
        for (int j = 0; j< Width; j++)
        {
            // niet zeker hoe dit werkt clear array[i, j] =  ;
            array[j, i] = block;
        }
    }
    void MoveDown(int i)
    {
        for (int y = i; i < Height; y++)
        {
            for (int j=0; j <Width; j++)
            {
                if(array[j, y] != null)
                {
                    array[j, y - 1] = array[j, y];
                    array[j, y] = null;
                    //mss nog een comand zoals array[j, y-1] = new Vector/ new posision
                }
            }
        }
    }
    

   
    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
    }
}
