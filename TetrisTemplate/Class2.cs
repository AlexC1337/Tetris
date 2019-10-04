using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    class Shapes : TetrisGrid
    {
        Texture2D[,] I = new Texture2D[4, 4]{{block, block, block, block},
                                                 {block, block, block, block},
                                                 {babyblue,babyblue,babyblue,babyblue},
                                                 {block, block, block, block}};
        Texture2D[,] S = new Texture2D[3, 3]{{block, orange, orange},
                                                 {orange, orange, block},
                                                 {block, block, block}};
        Texture2D[,] Z = new Texture2D[3, 3]{{orange, orange, block},
                                                 {block, orange, orange},
                                                 {block, block, block}};
        Texture2D[,] L = new Texture2D[3, 3]{{block, orange, block},
                                                 {block, orange, block},
                                                 {block, orange, orange}};
        Texture2D[,] J = new Texture2D[3, 3]{{block, orange, orange},
                                                 {block, orange, block},
                                                 {block, orange, block}};

        Texture2D[,] O = new Texture2D[2, 2]{{yellow, yellow},
                                                 {yellow, yellow}};

    }

    class T : Shapes
    {
        Texture2D[,] TShape = new Texture2D[3, 3]{{orange, orange, orange},
                                             {block, orange, block},
                                             {block, block, block}};
    }
}
