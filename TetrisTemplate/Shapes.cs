using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class Shapes
{
    public Vector2 gridpos;
    public Texture2D block = TetrisGame.ContentManager.Load<Texture2D>("block");
    public Texture2D yellow = TetrisGame.ContentManager.Load<Texture2D>("yellow");
    public Texture2D blue = TetrisGame.ContentManager.Load<Texture2D>("blue");
    public Texture2D red = TetrisGame.ContentManager.Load<Texture2D>("red");
    public Texture2D green = TetrisGame.ContentManager.Load<Texture2D>("green");
    public Texture2D purple = TetrisGame.ContentManager.Load<Texture2D>("purple");
    public Texture2D babyblue = TetrisGame.ContentManager.Load<Texture2D>("babyblue");
    public Texture2D orange = TetrisGame.ContentManager.Load<Texture2D>("orange");
    Texture2D[,] array;
    public Shapes()
    {
        Vector2 gridpos = new Vector2(TetrisGame.ScreenSize.X / 2, 0);
        Texture2D[,] array = new Texture2D[4,4];
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        int Height = array.Length;
        int Width = Height;
        Vector2 position = gridpos * block.Width;
        for (int y = 0; y != Height; y++)
        {
            for (int x = 0; x != Width; x++)
            {
                if (array[x, y] == block)
                {
                    spriteBatch.Draw(block, position, Color.White);   //als leeg teken lege cel
                }
                else
                {

                    spriteBatch.Draw(array[x, y], position, Color.White);   //als kleur, geef kleur

                }
                position.X += block.Width;
            }
            position.X = 0;
            position.Y += block.Height;
        }
    }
    public Texture2D[,] RotateRight(Texture2D[,] og) //TODO let op dat geen blokjes buiten het veld komen
    {
        int x = og.Length;
        Texture2D[,] array = new Texture2D[x,x];
        for (int i = 0; i < x; ++i)
        {
            for (int j = 0; j < x; ++j)
            {
                array[i, j] = og[x - j - 1, i];
            }
        }
        return array;
    }

    public Texture2D[,] RotateLeft(Texture2D[,] og)
    {
        for (int i = 0; i < 3; i++)
        {
            og = RotateRight(og);
        }
        return og;
    }
}

class T : Shapes
{
    public T()
    {
        Texture2D[,] array = new Texture2D[3, 3]{               {orange, orange, orange},
                                                                     {block, orange, block},
                                                                     {block, block, block}};
    }
}
class I : Shapes
{
    public I()
    {
        Texture2D[,] array = new Texture2D[4, 4]{   {block, block, block, block},
                                                        {block, block, block, block},
                                                        {babyblue,babyblue,babyblue,babyblue},
                                                        {block, block, block, block}};
    }
}

class J : Shapes
{
    public J()
    {
        Texture2D[,] array = new Texture2D[3, 3]{{block, orange, orange},
                                                     {block, orange, block},
                                                    {block, orange, block}};
    }
}
class L : Shapes
{
    public L()
    {
        Texture2D[,] array = new Texture2D[3, 3]{{block, orange, block},
                                                     {block, orange, block},
                                                     {block, orange, orange}};
    }
}


class S : Shapes
{
    public S()
    {
        Texture2D[,] array = new Texture2D[3, 3]{{block, orange, orange},
                                                     {orange, orange, block},
                                                     {block, block, block}};
    }
}

class Z : Shapes
{
    public Z()
    {
        Texture2D[,] array = new Texture2D[3, 3]{{orange, orange, block},
                                                     {block, orange, orange},
                                                    {block, block, block}};
    }
}

class O : Shapes
{
    public O()
    {
        Texture2D[,] array = new Texture2D[2, 2]{{yellow, yellow},
                                                    {yellow, yellow}};
    }
}
