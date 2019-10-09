using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

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
    static Random random;

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
    TetrisGrid grid;
    Shapes[] vormen = new Shapes[] { new T(), new J(), new L(), new S(), new Z(), new O(), new I() };

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Playing;
        currentShape = vormen[random.Next(vormen.Length)];
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        grid = new TetrisGrid();
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
    }

    public void Update(GameTime gameTime)
    {
        if (currentShape == null) 
        {
            currentShape = vormen[random.Next(vormen.Length)];
        }
        currentShape.gridpos.Y += gameTime.TotalGameTime.Seconds/60; // weet niet of dit werkt
        if (Keyboard.GetState().IsKeyDown(Keys.Down))  /// andere positie in code
        {
           // currentShape.gridpos.Y = currentShape.gridpos.Y = +1; //  Y grid + 1 
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            currentShape.gridpos.X = currentShape.gridpos.X -= +1; //  X grid -1 (naar links)
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            currentShape.gridpos.X = currentShape.gridpos.X += +1; //  X grid =1 (naar rechts)
        }

        //  if (currentShape.gridpos.Y == block(met kleur.Y) || currentShape.gridpos.Y == 20
        // {
        //      currentShape = block(met kleur)
        // }
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

    public void Sounds(ContentManager Content)
    {
       
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Play(Content.Load<Song>("Tetris"));
        
       // LineClear = Content.Load<SoundEffect>("clear"); /// moet bij de method die line clear doet
    }
    
}
