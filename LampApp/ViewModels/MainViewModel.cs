using LampApp.Commands;
using LampApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LampApp.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private Lamp lamp = new Lamp();

        #region Заголовок окна
        private string _Title = "LampApp";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value, "Title");
        }
        #endregion

        #region Статус программы

        private string _Status;
        /// <summary>Статус программы</summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value, "Status");
        }


        #endregion

        #region Слайдер скрость
        private int _Speed;
        /// <summary>
        /// Слайдер скорость
        /// </summary>
        public int Speed
        {
            get => _Speed;
            set
            {
                Set(ref _Speed, value, "Status");
                lamp.SendSetting("SPD", _Speed);
            }
        }
        #endregion

        #region Слайдер масштаб
        private int _Scale;
        /// <summary>
        /// Слайдер Масштаб
        /// </summary>
        public int Scale
        {
            get => _Scale;
            set
            {
                Set(ref _Scale, value, "Status");
                lamp.SendSetting("SCA", _Scale);
            }
        }
        #endregion

        #region Слайдер яркость
        private int _Brightness;
        /// <summary>
        /// Слайдер Яркость
        /// </summary>
        public int Brightness
        {
            get => _Brightness;
            set
            {
                Set(ref _Brightness, value, "Status");
                lamp.SendSetting("BRI", _Scale);
            }
        }
        #endregion

        #region Команда получение настроек
        /// <summary>
        /// Команда на получение настроек от лампы
        /// </summary>
        public ICommand GetSettings
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    lamp.GetSettings();
                });
            }
        }
        #endregion

        #region Команда получение настроек
        /// <summary>
        /// Команда на вкл/откл лампы
        /// </summary>
        public ICommand PowerSwitch
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    lamp.PowerOnOff();
                });
            }
        }
        #endregion
    }
}
