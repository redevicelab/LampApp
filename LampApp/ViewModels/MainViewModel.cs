using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LampApp.ViewModels
{
    public class MainViewModel : ViewModel
    {
        #region Заголовок окна
        private string _Title = "LampApp";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            //set 
            //{
            //    if (Equals(_Title, value)) return;
            //    _Title = value;
            //    OnPropertyChanged("Title");
            //}
            set => Set(ref _Title, value, "Title");
        }
        #endregion
    }
}
