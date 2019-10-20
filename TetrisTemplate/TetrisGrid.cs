using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{

    /// The position at which this TetrisGrid should be drawn.
    public Vector2 position;

    /// The aantal of grid elements in the x-direction.
    static public int Width { get { return 10; } }

    /// The aantal of grid elements in the y-direction.
    static public int Height { get { return 20; } }
    public Texture2D[,] array = new Texture2D[Width, Height];

    public Texture2D block = TetrisGame.ContentManager.Load<Texture2D>("block");// Sprites van de gebruikte blokjes
    public Texture2D yellow = TetrisGame.ContentManager.Load<Texture2D>("yellow");
    public Texture2D blue = TetrisGame.ContentManager.Load<Texture2D>("blue");
    public Texture2D red = TetrisGame.ContentManager.Load<Texture2D>("red");
    public Texture2D green = TetrisGame.ContentManager.Load<Texture2D>("green");
    public Texture2D purple = TetrisGame.ContentManager.Load<Texture2D>("purple");
    public Texture2D babyblue = TetrisGame.ContentManager.Load<Texture2D>("babyblue");
    public Texture2D orange = TetrisGame.ContentManager.Load<Texture2D>("orange"); //deze zooi moet misschien eigenlijk in een leuke init. oid. maar dat maakt alles stuk. als je dit nakijkt en denkt het beter te kunnen doen pls doe.
    
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
                array[x, y] = block; //maakt een leeg grid dat uit lege blokjes bestaat, "block" is hierbij de texture van een leeg blokje
            }
        }
        position = Vector2.Zero;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Vector2 drawPos = position;
        for (int y = 0; y != Height; y++)
        {
            for (int x = 0; x != Width; x++)
            {
                spriteBatch.Draw(array[x, y], drawPos, Color.White);  //tekent voor elk cel het corresponderende blokje in de grid.       
                drawPos.X += block.Width;
            }
            drawPos.X = 0;
            drawPos.Y += block.Height;
        }
    }

    public void CheckfullLine() //gaat elke rij na of deze een leeg block bevat, als dit niet een geval is, is de rij dus vol en wordt er een collapse in werking gestelt
    {
        Vector2 loopPos = position;
        for (int y = Height-1; y != 0; y--)
        {
            bool fullrow = true;
            for (int x = 0; x != Width; x++)
            {
                if (array[x, y] == block)
                {
                    fullrow = false;
                }
                loopPos.X += block.Width;
            }
            if (fullrow)
            {
                Collapse(y);
                y++; //anders skipt ie rijen als er meerdere tegelijk zijn
                GameWorld.Score += 10;
                GameWorld.Scorecount += 10;
                GameWorld.LineClear.Play();
            }
            loopPos.X = 0;
            loopPos.Y += block.Height;
        }
    }
    protected void Collapse(int yArg) //maakt van een volle rij een rij met lege blocks en schuift deze als het ware omhoog door continu met de rij erboven te swappen, hierdoor schuiven de rijen erboven dus ook meteen naar beneden.
    {
        Vector2 loopPos = position;
        for (int y = yArg; y != 0; y--)
        {
            for (int x = 0; x != Width; x++)
            {
                array[x, y] = array[x, y-1];
                array[x, y-1] = block;
                loopPos.X += block.Width;
            }

            loopPos.X = 0;
            loopPos.Y += block.Height;
        }
    }
}
