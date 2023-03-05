using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Exam_WPF
{
    public class VM : INotifyPropertyChanged
    {
        private Recept_model recept;
        public ObservableCollection<Recept_model> Recepts { get; set; } // все рецепты
        private Command save;

        public Command Save
        {
            get
            {
                return save ??
                  (save = new Command(obj =>
                  {
                      Recept_model rec = new Recept_model();
                      Recepts.Insert(0, rec);
                      recept = rec;
                  }));
            }
        }

        public Recept_model SelectedRecept
        {
            get { return recept; }
            set
            {
                recept = value;
                OnPropertyChanged("SelectedRecept");
            }
        }

        public VM()
        {
            Recepts = new ObservableCollection<Recept_model>();
        }

        //сохранение
        public void Save_to_file()
        {
            File.Delete("Recepts.json"); // для верности удалим старые
            using (FileStream fs = new FileStream("Recepts.json", FileMode.Create, FileAccess.ReadWrite))
            {
                JsonSerializer.Serialize(fs, Recepts);
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
