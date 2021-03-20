using System;
using System.Collections.Generic;
using System.Numerics;
using System.Collections;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    public static class RandomExtensions
    {
        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }

    [Serializable]
    public class V4DataCollection : V4Data, IEnumerable<DataItem>, ISerializable
    {
        public override IEnumerator<DataItem> GetEnumerator()
        {
            foreach (var item in dict)
                yield return new DataItem(item.Key, item.Value);
        }

        public Dictionary<Vector2, Complex> dict { set; get; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            float[] x = new float[dict.Count];
            float[] y = new float[dict.Count];
            Complex[] values = new Complex[dict.Count];
            int i = 0;
            foreach (var item in dict)
            {
                x[i] = item.Key.X;
                y[i] = item.Key.Y;
                values[i] = item.Value;
                i++;
            }
            info.AddValue("x", x);
            info.AddValue("y", y);
            info.AddValue("values", values);
            info.AddValue("ID", Info);
            info.AddValue("W", Frequency);
        }

        public V4DataCollection(SerializationInfo info, StreamingContext context):
            base(info.GetValue("ID", typeof(string)) as string, (double)info.GetValue("W", typeof(double)))
        {
            float[] x = info.GetValue("x", typeof(float[])) as float[];
            float[] y = info.GetValue("y", typeof(float[])) as float[];
            Complex[] values = info.GetValue("values", typeof(Complex[])) as Complex[];
            dict = new Dictionary<Vector2, Complex>();
            for (int i = 0; i < x.Length; i++)
            {
                dict.Add(new Vector2(x[i], y[i]), values[i]);
            }
        }

        public V4DataCollection(string id, double w) : base(id, w)
        {
            dict = new Dictionary<Vector2, Complex>();
        }

        public void InitRandom(int nItems, float xmax, float ymax, double minValue, double maxValue)
        {
            Random random = new Random();
            for (int i = 0; i < nItems; i++)
            {
                float x = Convert.ToSingle(random.NextDouble(0, xmax));
                float y = Convert.ToSingle(random.NextDouble(0, ymax));
                double real = random.NextDouble(minValue, maxValue);
                double imaginary = random.NextDouble(minValue, maxValue);
                Vector2 v = new Vector2(x, y);
                Complex c = new Complex(real, imaginary);
                dict.Add(v, c);
            }
        }

        public override Complex[] NearMax(float eps)
        {
            Complex[] res = new Complex[0];
            int index = 0;
            double[] tmp = new double[dict.Count];
            foreach (Complex c in dict.Values)
            {
                tmp[index] = Complex.Abs(c);
                index++;
            }
            double max = tmp[0];
            for (int i = 0; i < dict.Count; i++)
            {
                max = Math.Max(max, tmp[i]);
            }

            index = 0;
            foreach (Complex c in dict.Values)
            {
                if (Math.Abs(max - Complex.Abs(c)) <= eps)
                {
                    Array.Resize(ref res, index + 1);
                    res[index] = c;
                    index++;
                }
            }
            return res;
        }
        public override string ToLongString()
        {
            string res = ToString();
            foreach (var item in dict)
            {
                res = res + $"\ncoordinates={item.Key}; complex value={item.Value}";
            }
            return res;
        }

        public override string ToString()
        {
            return $"V4DataCollection: info={Info}; frequency={Frequency}; num of elements in dict = {dict.Count}";
        }

        public override string ToLongString(string format)
        {
            string res = $"V4DataCollection: info={Info}; frequency={Frequency.ToString(format)}; num of elements in dict = {dict.Count}";
            foreach (var item in dict)
            {
                res = res + $"\ncoordinates={item.Key.ToString(format)}; complex value={item.Value.ToString(format)}; abs. value = {item.Value.Magnitude.ToString(format)}";
            }
            return res;
        }

        /*
        * В файле filename в текстовом виде хранится вся информация: 
        * с разделителями в виде новой строки хранятся данные: info, frequency, 
        * значение координаты точки x, значение координаты точки y, 
        * комплексное значение поля по x, комплексное значение по y, 
        * и далее снова координаты точек и комплексные значения в них. 
        */
        public V4DataCollection(string filename) : base(null, 0)
        {
            String line;
            FileStream fs = null;
            CultureInfo myCI = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
                fs = new FileStream(filename, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                line = sr.ReadLine();
                if (line != null) Info = line;
                else throw new Exception("No info provided.");

                line = sr.ReadLine();
                if (line != null) Frequency = Convert.ToDouble(line);
                else throw new Exception("No frequency provided.");

                dict = new Dictionary<Vector2, Complex>();
                //Continue to read until you reach end of file
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line == null) throw new Exception("Not enough information provided.");
                    float x = Convert.ToSingle(line);
                    line = sr.ReadLine();
                    if (line == null) throw new Exception("Not enough information provided.");
                    float y = Convert.ToSingle(line);
                    line = sr.ReadLine();
                    if (line == null) throw new Exception("Not enough information provided.");
                    double real = Convert.ToDouble(line);
                    line = sr.ReadLine();
                    if (line == null) throw new Exception("Not enough information provided.");
                    double imaginary = Convert.ToDouble(line);
                    Vector2 v = new Vector2(x, y);
                    Complex c = new Complex(real, imaginary);
                    dict.Add(v, c);

                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.Message);
            }
            finally
            {
                if (fs != null) fs.Close();
                CultureInfo.CurrentCulture = myCI;
            }
        }
    }
}
