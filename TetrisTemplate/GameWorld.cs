using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
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
    Shapes currentShape, nextShape;

    /// The random-number generator of the game.
    public static Random Random { get { return random; } }
    static Random random = new Random(); 
    /// The main font of the game.
    SpriteFont font;
    Texture2D background;
    /// The current game state.
    GameState gameState;
    /// The main grid of the game.
    public TetrisGrid grid;
    static double speed;
    static double timeSinceLastMove = 0;
    const double speedModifier = 0.7; //speed modifier changes difficulty, modifier 0.6 gives an endspeed of 4 times the normal speed, 0,7 3 times, 0.8 2 times.
    protected static SoundEffect RotateSound;
    public static SoundEffect LineClear;
    public static int score;
    public static int totalscore;
    static int level;

    public void puntenSpeed() // level, snelheid verandering
    {
        if (score == 100)
        {
            speed = speed * speedModifier + 0.1; //speed gradually changes to a set point, at a certain point the speed stops increasing because the limit has been reached.
            score = 0;
            level++;
        }
    }
    public GameWorld(ContentManager Content)
    {
        RotateSound = Content.Load<SoundEffect>("Ttrs---Rotate");
        LineClear = Content.Load<SoundEffect>("Ttrs---Clear-Line");
        gameState = GameState.Playing;
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Space))  // andere positie in code
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
        if (inputHelper.KeyPressed(Keys.A) || inputHelper.KeyPressed(Keys.Up))
        {
            currentShape.RotateLeft();
            RotateSound.Play();
            if (Collision())
            {
                currentShape.RotateRight();
            }
        }
        if (inputHelper.KeyPressed(Keys.D))
        {
            currentShape.RotateRight();
            RotateSound.Play();
            if (Collision())
            {
                currentShape.RotateLeft();
            }
        }

        if(inputHelper.KeyPressed(Keys.R) && gameState == GameState.GameOver)
        {
            Reset();
        }
    }

    public void Initialise() // bakt aan het begin een gridje, een vormpje en andere dingen die ingeladen moeten worden.
    {

        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        background = TetrisGame.ContentManager.Load<Texture2D>("background");
    }

    protected void NewShape() //pakt een nieuw random tetrisvormpje
    {
        switch (random.Next(7))//wilde eerst random object uit array maar dit werkte niet, daarom dus deze onelegante switch
        {
            case 0:
                nextShape = new T();
                break;
            case 1:
                nextShape = new J();
                break;
            case 2:
                nextShape = new L();
                break;
            case 3:
                nextShape = new I();
                break;
            case 4:
                nextShape = new O();
                break;
            case 5:
                nextShape = new S();
                break;
            case 6:
                nextShape = new Z();
                break;
        }
        nextShape.gridpos = new Point(14, 1);
    }

    public void Update(GameTime gameTime)
    {
        if (gameState == GameState.Playing)
        {
            timeSinceLastMove += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastMove >= speed)
            {
                currentShape.gridpos.Y += 1;
                timeSinceLastMove -= speed;   //zorgt er voor dat het blokje consistent naar beneden valt
                if (Collision())            //als het blokje automatisch naar beneden gaat en in de stapel met blokjes zit, wordt de bool Collision true, schuift het blokje weer 1 positie omhoog en wordt onderdeel van de stapel
                {
                    currentShape.gridpos.Y -= 1;
                    JoinGrid();
                    puntenSpeed();
                }
            }
        }
       
    }
    public bool Collision() // kijkt of het blokje op een plek zit waar hij mag zijn, dus niet buiten het speelveld of in een blokje in het grid
    {
        bool collision = false; //standaard is er geen collision
        Texture2D[,] array = currentShape.array;
        Point gridpos = currentShape.gridpos;
        int length = array.GetLength(1);
        for (int y = 0; y < length; y++)
        {
            for (int x = 0; x < length; x++)
            {
               int blockX = gridpos.X + x;
               int blockY = gridpos.Y + y;
                if (array[x, y].Name != "block") //"block" is een lege plek in de array, dus moet er alleen gekeken worden naar entries die niet leeg zijn.
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
                int blockX = currentShape.gridpos.X + x;//geeft de positie van de 4 blokjes waaruit een tetrisblokje is opgebouwd op het tetrisgrid
                int blockY = currentShape.gridpos.Y + y;
                if (currentShape.array[x, y].Name != "block")
                {
                    grid.array[blockX, blockY] = currentShape.array[x, y]; //vervangt het grid met blokje
                }
            }
        }
        currentShape = nextShape;
        currentShape.gridpos = new Point(4, 0);
        NewShape();
        if (Collision())
        {
            gameState = GameState.GameOver; //als er net een nieuw vormpje spawnt en hij collide al meteen met het grid is het game over, helaas pindakaas, dommage peanuttefromage
        }
        grid.CheckfullLine();
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        if (gameState == GameState.Playing)
        {
            spriteBatch.Draw(background, new Vector2(TetrisGame.ScreenSize.X / 2, 0), Color.White);//background van https://www.zedge.net/wallpaper/1d9b81a3-c7cc-38e8-a176-df7111aad04e
            grid.Draw(gameTime, spriteBatch);
            currentShape.Draw(gameTime, spriteBatch);
            nextShape.Draw(gameTime, spriteBatch);
            scoreDisplay(gameTime, spriteBatch);
        }
        if(gameState == GameState.GameOver)
        {
            gameOver(gameTime, spriteBatch);
        }
        spriteBatch.End();
    }

    protected void scoreDisplay(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, "Level: " + level, new Vector2(TetrisGrid.Width * grid.block.Width + 20, 50), Color.White);
        spriteBatch.DrawString(font, "Score: " + totalscore, new Vector2(TetrisGrid.Width * grid.block.Width + 20, 70), Color.White);
        spriteBatch.DrawString(font, "Next Shape: ", new Vector2(TetrisGrid.Width * grid.block.Width + 80, 5), Color.White);
    }

    protected void gameOver(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, "Game Over, press R to restart", new Vector2(TetrisGame.ScreenSize.X /2 -100, TetrisGame.ScreenSize.Y/2), Color.Black);
        spriteBatch.DrawString(font, "Level: " + level, new Vector2(TetrisGame.ScreenSize.X / 2 - 100, TetrisGame.ScreenSize.Y / 2 + 20), Color.Black);
        spriteBatch.DrawString(font, "Score: " + totalscore, new Vector2(TetrisGame.ScreenSize.X / 2 - 100, TetrisGame.ScreenSize.Y / 2 + 40), Color.Black);
    }
    public void Reset()
    {
        gameState = GameState.Playing;
        score = 0;
        level = 1;
        speed = 1;
        totalscore = 0;
        grid = new TetrisGrid();
        NewShape();
        currentShape = nextShape;
        currentShape.gridpos = new Point(4, 0);
        NewShape();
        int arraySize = currentShape.array.Length;
        Texture2D[,] array = currentShape.array;
    }

    
    
}
