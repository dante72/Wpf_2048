using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_2048
{
    public class MyModel
    {
        private Random rnd;
        public MyModel()
        {
            rnd = new Random();
            MyCollection = new ObservableCollection<int> { };
            for (int i = 0; i < 16; i++)
                MyCollection.Add(rnd.Next(0, 10));
        }

        public ObservableCollection<int> MyCollection { get; }
        public void myCollectionCollection()
        {
            for (int i = 0; i < MyCollection.Count; i++)
                MyCollection[i] = rnd.Next(0, 10);
        }
    }
}

