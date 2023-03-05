using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Exam_WPF 
{
    public class Recept_model: INotifyPropertyChanged
    {
        private string name; //название
        private string content; // содержание
        private List<string> ingridients; // интридиенты
        private string categoty; // категория
        private string kitchen; // кухня

        public String Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public String Content
        {
            get { return content; }
            set
            {
                content = value;
                OnPropertyChanged("Content");
            }
        }

        public List<string> Ingridients
        {
            get { return ingridients; }
            set
            {
                ingridients = value;
                OnPropertyChanged("Ingridients");
            }
        }

        public String Category
        {
            get { return categoty; }
            set
            {
                categoty = value;
                OnPropertyChanged("Categoty");
            }
        }

        public String Kitchen
        {
            get { return kitchen; }
            set
            {
                kitchen = value;
                OnPropertyChanged("Kitchen");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
