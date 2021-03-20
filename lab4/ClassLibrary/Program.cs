using System;
using System.Collections.Generic;
using System.Numerics;
using System.Collections;
using System.ComponentModel;
using System.IO;
using ClassLibrary;

namespace pr
{

    /*
    class Program
    {
        static void HandleDataChanged(object sender, DataChangedEventArgs args)
        {
            Console.WriteLine(args.ToString());
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine();
                V4MainCollection obj = new V4MainCollection();
                obj.DataChanged += HandleDataChanged;
                obj.AddDefaults();
                //Console.WriteLine(obj.ToString());
                Grid2D temp = new Grid2D(0.5f, 0.7f, 1, 2);
                V4DataOnGrid addElement = new V4DataOnGrid("123", 111d, temp);
                addElement.InitRandom(4.5, 6.1);
                obj.Add(addElement);
                obj[1] = new V4DataCollection("123", 134d);
                obj[1].Frequency = 1876d;
                obj[2].Info = "newData";
                //Console.WriteLine(obj.ToString());
                obj.Remove("123", 111d);
                V4DataOnGrid newEl = new V4DataOnGrid("123", 111d, temp);
                obj[1] = newEl;
                newEl.Info = "12321342131";
                //Console.WriteLine(obj.ToString());
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.Message);
                Console.ReadKey();
            }
        }
    }*/
}
