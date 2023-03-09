﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;


namespace Exam_WPF
{
    public class VM : INotifyPropertyChanged
    {
        private Recept_model recept;
        public ObservableCollection<Recept_model> Recepts { get; set; } // все рецепты
        private Command save;
        private Command del;
        private Command export_json;
        private Command export_pdf;
        
        //команда сохранить
        public Command Save
        {
            get
            {
                return save ??
                  (save = new Command(obj =>
                  {
                      Recept_model rec = new Recept_model();
                      recept = rec;
                      Recepts.Insert(0, rec);
                      
                  }));
            }
        }

        public Command Export_pdf
        {
            get
            {
                return export_pdf ??
                    (export_pdf = new Command(obj =>
                    {
                        Recept_model to_ex = (Recept_model)obj;
                        to_ex.Export_to_dpf();
                    }
                    ));
            }
        }

        //команда удалить
        public Command RemoveCommand // команда на удаление
        {
            get
            {
                return del ??
                    (del = new Command(obj =>
                    {
                        Recept_model rec = obj as Recept_model;
                        if (rec != null)
                            Recepts.Remove(rec);
                    },
                    (obj) => Recepts.Count > 0));
            }
        }

        public Command Export_json
        {
            get
            {
                return export_json ??
                    (export_json = new Command(obj =>
                    {
                        this.Save_to_file();
                    }
                    ));
            }
        }

        //выбранный рецепт
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
            if (File.Exists("Recepts.json"))
                Load_from_file();
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

        //загрузка
        public void Load_from_file()
        {
            if (File.Exists("Recepts.json"))
            {
                string source = File.ReadAllText("Recepts.json");
                Recepts = JsonSerializer.Deserialize<ObservableCollection<Recept_model>>(source);
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
