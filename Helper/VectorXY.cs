using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Helper
{
    public readonly record struct VectorXY(int x, int y)
    {
        public static readonly VectorXY Left = new VectorXY(0, -1);
        public static readonly VectorXY Right = new VectorXY(0, +1);

        public static VectorXY operator +(VectorXY left, VectorXY right)
        {
            return new VectorXY(left.x + right.x, left.y + right.y);
        }
        public static VectorXY operator -(VectorXY left, VectorXY right)
        {
            return new VectorXY(left.x - right.x, left.y - right.y);
        }
        public static VectorXY operator -(VectorXY vec)
        {
            return new VectorXY(-vec.x, -vec.y);
        }

        public readonly VectorXY Scale(int factor)
        {
            return new VectorXY(x * factor, y * factor);
        }
        public readonly VectorXY RotatedLeft()
        {
            return new VectorXY(-y, x);
        }
        public readonly VectorXY RotatedRight()
        {
            return new VectorXY(y, -x);
        }

        public readonly int ManhattanMetric()
        {
            return Math.Abs(x) + Math.Abs(y);
        }
        public readonly VectorXY NextLeft()
        {
            return this + Left;
        }
        public readonly VectorXY NextRight()
        {
            return this + Right;
        }
    }
}