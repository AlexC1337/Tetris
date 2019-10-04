using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


    class Shapes : TetrisGrid 
    {
    
    Vector2 postitie = new Vector2(0,0);
        
        public Texture2D[,] RotateRight(Texture2D[,] og)
        {
            int x = og.Length;
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
            for(int i = 0; i < 3; i++)
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
            Texture2D[,] shape = new Texture2D[3, 3]{               {orange, orange, orange},
                                                                     {block, orange, block},
                                                                     {block, block, block}};
        }
    }
    class I : Shapes
    {
        public I()
        {
            Texture2D[,] shape = new Texture2D[4, 4]{   {block, block, block, block},
                                                        {block, block, block, block},
                                                        {babyblue,babyblue,babyblue,babyblue},
                                                        {block, block, block, block}};
        }
    }

    class J : Shapes
    {
        public J()
        {
            Texture2D[,] shape = new Texture2D[3, 3]{{block, orange, orange},
                                                     {block, orange, block},
                                                    {block, orange, block}};
        }
    }
    class L : Shapes
    {
        public L()
        {
            Texture2D[,] shape = new Texture2D[3, 3]{{block, orange, block},
                                                     {block, orange, block},
                                                     {block, orange, orange}};
        }
    }


    class S : Shapes
    {
        public S()
        {
            Texture2D[,] shape = new Texture2D[3, 3]{{block, orange, orange},
                                                     {orange, orange, block},
                                                     {block, block, block}};
        }
    }

    class Z : Shapes
    {
        public Z()
        {
            Texture2D[,] shape = new Texture2D[3, 3]{{orange, orange, block},
                                                     {block, orange, orange},
                                                    {block, block, block}};
        }
    }

    class O : Shapes
    {
        public O() {
            Texture2D[,] shape = new Texture2D[2, 2]{{yellow, yellow},
                                                    {yellow, yellow}};
        }
    }
    
}
