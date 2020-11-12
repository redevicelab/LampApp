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
        private Effect effect = new Effect();

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

        #region Адресс
        /// <summary>
        /// Поле ввода адреса
        /// </summary>
        private string _Address = "127.0.0.1";
        public string Address
        {
            get => _Address;
            set => Set(ref _Address, value, "Status");
        }
        #endregion

        #region Порт
        /// <summary>
        /// Поле ввода порта
        /// </summary>
        private int _Port;
        public int Port
        {
            get => _Port;
            set => Set(ref _Port, value, "Status");
        }
        #endregion

        #region Combobox Эффекты
        private Effect _SelectedEffect;
        public List<Effect> listEffects { get; set; }
        public Effect SelectedEffect
        {
            get { return _SelectedEffect; }
            set
            {
                _SelectedEffect = value;
                OnPropertyChanged("SelectedPhone");
                Status = lamp.SendSetting("EFF", _SelectedEffect.NumberEffect);
            }
        }


        #endregion

        #region Кнопка включения
        /// <summary>
        /// Состояние кнопки Power
        /// </summary>
        private bool _Power = false;
        public bool Power
        {
            get => _Power;
            set => Set(ref _Power, value, "Power");
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
                Status = lamp.SendSetting("SPD", _Speed);
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
                Status = lamp.SendSetting("SCA", _Scale);
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
                Status = lamp.SendSetting("BRI", _Brightness);
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

        #region Команда на вкл/откл лампы
        /// <summary>
        /// Команда на вкл/откл лампы
        /// </summary>
        public ICommand PowerSwitch
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                
                    Power = lamp.PowerOnOff(Power);
                    Status = Power.ToString();
                });
            }
        }
        #endregion

        public MainViewModel()
        {
            listEffects = new List<Effect>
            {
                new Effect {NumberEffect = 0, NameEffect = "Конфити"},
                new Effect {NumberEffect = 1, NameEffect = "Огонь"},
                new Effect {NumberEffect = 2, NameEffect = "Радуга вертикальная"},
                new Effect {NumberEffect = 3, NameEffect = "Радуга горизонтальная"},
                new Effect {NumberEffect = 4, NameEffect = "Менающиеся цвета"},
                new Effect {NumberEffect = 5, NameEffect = "Безумие 3D"},
                new Effect {NumberEffect = 6, NameEffect = "Облака 3D"},
                new Effect {NumberEffect = 7, NameEffect = "Лава 3D"},
                new Effect {NumberEffect = 8, NameEffect = "Плазма 3D"},
                new Effect {NumberEffect = 9, NameEffect = "Радуга 3D"},
                new Effect {NumberEffect = 10, NameEffect = "Павлин 3D"},
                new Effect {NumberEffect = 11, NameEffect = "Лес 3D"},
                new Effect {NumberEffect = 12, NameEffect = "Океан 3D"},
                new Effect {NumberEffect = 13, NameEffect = "Цвет"},
                new Effect {NumberEffect = 14, NameEffect = "Снег"},
                new Effect {NumberEffect = 15, NameEffect = "Матрица"},
                new Effect {NumberEffect = 16, NameEffect = "Светлячки"}
            };
        }
    }
}
