using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using Aspose;
using Aspose.Pdf;
using System.Windows;

namespace Exam_WPF 
{
    public class Recept_model: INotifyPropertyChanged
    {
        private string name; //название
        private string content; // содержание
        private string ingridients; // интридиенты
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

        public string Ingridients
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

        public override string ToString()
        {
            return Name;
        }

        public void Export_to_dpf()
        {
            // Initialize document object
            Document document = new Document();
            // Add page
            Page page = document.Pages.Add();
            // Add text to new page
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(name));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(categoty));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(Kitchen));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Ингридиенты: " + ingridients));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Содержание:\n " + content));
             //Save updated PDF
            document.Save($"{name}.pdf");
            if (File.Exists($"{name}.pdf"))
                MessageBox.Show($"Успешно создан {name}.pdf");
        }

      

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
