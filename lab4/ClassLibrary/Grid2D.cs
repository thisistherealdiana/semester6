using System;
namespace ClassLibrary
{
    [Serializable]
    public struct Grid2D
    {
        public float step_x { get; set; }
        public int num_x { get; set; }
        public float step_y { get; set; }
        public int num_y { get; set; }

        public Grid2D(float st_x = 0, float st_y = 0, int n_x = 0, int n_y = 0)
        {
            step_x = st_x;
            step_y = st_y;
            num_x = n_x;
            num_y = n_y;
        }
        public override string ToString()
        {
            return $"step_x={step_x}; step_y={step_y}; num_x={num_x}; num_y={num_y}";
        }
        public string ToString(string format)
        {
            return $"step_x={step_x.ToString(format)}; step_y={step_y.ToString(format)}; num_x={num_x}; num_y={num_y}";
        }
    }
}
