using System;
using System.Collections.Generic;
using System.Numerics;
using System.Collections;
using System.ComponentModel;

namespace ClassLibrary
{
    [Serializable]
    public abstract class V4Data : IEnumerable<DataItem>, INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
        }


        private string info;
        private double frequency;

        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                OnPropertyChanged("Info");

            }
        }

        public double Frequency
        {
            get { return frequency; }
            set
            {
                frequency = value;
                OnPropertyChanged("Frequency");
            }
        }

        public V4Data(string id, double w)
        {
            Info = id;
            Frequency = w;
        }

        public abstract Complex[] NearMax(float eps);
        public abstract string ToLongString();
        public abstract string ToLongString(string format);

        public override string ToString()
        {
            return $"info={Info}; frequency={Frequency}";
        }

        public abstract IEnumerator<DataItem> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
