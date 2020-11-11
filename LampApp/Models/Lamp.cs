using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LampApp.ViewModels;

namespace LampApp.Models
{
    public class Lamp : ViewModel
    {
        private string _Address;
        private int _Port;
        private bool _Power;

        public string Address
        {
            get => _Address;
            set => Set(ref _Address, value, "Status");
        }

        public int Port
        {
            get => _Port;
            set => Set(ref _Port, value, "Status");
        }

        public bool Power
        {
            get => Power;
            set => _Power = value;
        }

        /// <summary>
        /// Метод получения натроек от лампы
        /// </summary>
        public void GetSettings()
        {

        }
        /// <summary>
        /// Метод отправки сообщения на лампу
        /// </summary>
        /// <param name="settingName">Название настроки</param>
        /// <param name="value">Значение</param>
        public void SendSetting(string settingName, int value)
        {

        }

        public void PowerOnOff()
        {

        }
    }
}
