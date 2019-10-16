using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// A class for representing the game world.
/// This contains the grid, the falling block, and everything else that the player can see/do.
/// </summary>
class GameWorld
{
    /// <summary>
    /// An enum for the different game states that the game can have.
    /// </summary>
    enum GameState
    {
        Playing,
        GameOver
    }
    Shapes currentShape;

    /// <summary>
    /// The random-number generator of the game.
    /// </summary>
    public static Random Random { get { return random; } }
    static Random random = new Random();

    /// <summary>
    /// The main font of the game.
    /// </summary>
    SpriteFont font;

    /// <summary>
    /// The current game state.
    /// </summary>
    GameState gameState;

    /// <summary>
    /// The main grid of the game.
    /// </summary>
    public TetrisGrid grid;
    //  Shapes[] vormen = new Shapes[] { new T(), new J(), new L(), new S(), new Z(), new O(), new I()};
    double speed = 1;
    double timeSinceLastMove = 0;

    public GameWorld()
    {
        gameState = GameState.Playing;
        // currentShape = vormen[random.Next(vormen.Length)];
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Down))  /// andere positie in code
        {
            currentShape.gridpos.Y += 1; //  Y grid + 1
            if (Collision())
            {
                currentShape.gridpos.Y -= 1;
            }
        }
        if (inputHelper.KeyPressed(Keys.Left))
        {
            currentShape.gridpos.X -= 1; //  X grid -1 (naar links)
            if (Collision())
            {
                currentShape.gridpos.X += 1;
            }
        }
        if (inputHelper.KeyPressed(Keys.Right))
        {
            currentShape.gridpos.X += 1; //  X grid =1 (naar rechts)
            if (Collision())
            {
                currentShape.gridpos.X -= 1;
            }
        }
        if (inputHelper.KeyPressed(Keys.A))
        {
            currentShape.RotateLeft();
            if (Collision())
            {
                currentShape.RotateRight();
            }
        }
        if (inputHelper.KeyPressed(Keys.D))
        {
            currentShape.RotateRight();
            if (Collision())
            {
                currentShape.RotateLeft();
            }
        }
    }

    public void Initialise()
    {  //wilde eerst random object uit array maar dit werkte niet, daarom dus deze onelegante switch

        MakeShape();
        int arraySize = currentShape.array.Length;
        Texture2D[,] array = currentShape.array;

        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        grid = new TetrisGrid();
    }

    protected void MakeShape()
    {
        switch (random.Next(7))
        {
            case 0:
                currentShape = new T();
                break;
            case 1:
                currentShape = new J();
                break;
            case 2:
                currentShape = new L();
                break;
            case 3:
                currentShape = new I();
                break;
            case 4:
                currentShape = new O();
                break;
            case 5:
                currentShape = new S();
                break;
            case 6:
                currentShape = new Z();
                break;
        }
    }

    public void Update(GameTime gameTime)
    {

        timeSinceLastMove += gameTime.ElapsedGameTime.TotalSeconds;
        if (timeSinceLastMove >= speed)
        {
            currentShape.gridpos.Y += 1;
            timeSinceLastMove -= speed;
            if (Collision())
            {
                currentShape.gridpos.Y -= 1;
                JoinGrid();
            }
        }
    }
    public bool Collision()
    {
        bool collision = false;
        Texture2D[,] array = currentShape.array;
        Point gridpos = currentShape.gridpos;
        Point RelPos = gridpos;
        int length = array.GetLength(1);
        //switch (direction)

        for (int y = 0; y < length; y++)
        {
            for (int x = 0; x < length; x++)
            {
               int blockX = gridpos.X + x;
               int blockY = gridpos.Y + y;
                if (array[x, y].Name != "block")
                {
                    if (blockX < 0 || blockX > 9 || blockY < 0 || blockY > 19 || grid.array[blockX, blockY].Name != "block")//kijk of shape niet buiten grid raakt en dat shape niet op een gevuld gridblok staat
                    {
                        collision = true;

                    }
                }

            }
        }
        return collision;
    }

    public void JoinGrid()
    {
        Texture2D[,] array = currentShape.array;
        int length = array.GetLength(1);
        for (int y = 0; y < length; y++)
        {
            for (int x = 0; x < length; x++)
            {
                int blockX = currentShape.gridpos.X + x;
                int blockY = currentShape.gridpos.Y + y;
                if (currentShape.array[x, y].Name != "block")
                {
                    grid.array[blockX, blockY] = currentShape.array[x, y];
                }
            }
        }
        MakeShape();
       // grid.CheckfullLine();
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        grid.Draw(gameTime, spriteBatch);
        currentShape.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }

    public void Reset()
    {
    }

}
