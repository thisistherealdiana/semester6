using System;
using System.Collections.Generic;
using System.Numerics;

namespace ClassLibrary
{
    [Serializable]
    public class V4DataOnGrid : V4Data, IEnumerable<DataItem>
    {
        public override IEnumerator<DataItem> GetEnumerator()
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Vector2 v = new Vector2(i * grid.step_x, j * grid.step_y);
                    Complex c = array[i, j];
                    yield return new DataItem(v, c);
                }
            }
        }

        public Grid2D grid { set; get; }
        public Complex[,] array { set; get; }

        public V4DataOnGrid(string id, double w, Grid2D gr) : base(id, w)
        {
            grid = gr;
            array = new Complex[grid.num_x, grid.num_y];
        }

        public void InitRandom(double minValue, double maxValue)
        {
            //Создание объекта для генерации чисел
            Random random = new Random();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    double real = random.NextDouble(minValue, maxValue);
                    double imaginary = random.NextDouble(minValue, maxValue);
                    Complex c = new Complex(real, imaginary);
                    //Console.WriteLine(c);
                    array[i, j] = c;
                }
            }
        }

        //оператор преобразования
        public static explicit operator V4DataCollection(V4DataOnGrid v4)
        {
            V4DataCollection res = new V4DataCollection(v4.Info, v4.Frequency);
            Dictionary<Vector2, Complex> dct = new Dictionary<Vector2, Complex>();
            //Vector2 v = new Vector2(v4.grid.step_x, v4.grid.step_y);
            //Complex c = new Complex(real, imaginary)
            //int num = v4.array.GetLength(0) * v4.array.GetLength(1);
            float coord_x = 0;
            float coord_y = 0;
            for (int i = 0; i < v4.array.GetLength(0); i++)
            {
                for (int j = 0; j < v4.array.GetLength(1); j++)
                {
                    Vector2 v = new Vector2(coord_x + i * v4.grid.step_x, coord_y + j * v4.grid.step_y);
                    Complex c = new Complex(v4.array[i, j].Real, v4.array[i, j].Imaginary);
                    //Console.WriteLine(v);
                    //Console.WriteLine(c);
                    dct.Add(v, c);
                }
            }
            res.dict = dct;
            return res;
        }

        public override Complex[] NearMax(float eps)
        {
            Complex[] res = new Complex[0];
            int index = 0;
            double max = Complex.Abs(array[0, 0]);
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    max = Math.Max(max, Complex.Abs(array[i, j]));
                }
            }
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (Math.Abs(max - Complex.Abs(array[i, j])) <= eps)
                    {
                        Array.Resize(ref res, index + 1);
                        res[index] = array[i, j];
                        index++;
                    }
                }
            }
            return res;

        }

        public override string ToLongString()
        {
            string res = ToString();
            float coord_x = 0;
            float coord_y = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    res = res + $"\ncoord=<{coord_x + i * grid.step_x}, {coord_y + j * grid.step_y}>; value={array[i, j]}";
                }
            }
            return res;
        }

        public override string ToString()
        {
            return $"V4DataOnGrid: info={Info}; frequency={Frequency}; step_x={grid.step_x}; step_y={grid.step_y}; num_x={grid.num_x}; num_y={grid.num_y}";
        }

        public override string ToLongString(string format)
        {
            string res = $"V4DataOnGrid: info={Info}; frequency={Frequency.ToString(format)}; step_x={grid.step_x.ToString(format)}; step_y={grid.step_y.ToString(format)}; num_x={grid.num_x}; num_y={grid.num_y}";
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    res = res + $"\ncoord=<{(i * grid.step_x).ToString(format)}, {(j * grid.step_y).ToString(format)}>; value={(array[i, j]).ToString(format)}; abs. value = {array[i, j].Magnitude.ToString(format)}";
                }
            }
            return res;
        }

    }
}