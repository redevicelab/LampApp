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
        public string GetSettings()
        {
            return "GET";
        }
        /// <summary>
        /// Метод отправки сообщения на лампу
        /// </summary>
        /// <param name="settingName">Название настроки</param>
        /// <param name="value">Значение</param>
        public string SendSetting(string settingName, int value)
        {
            return settingName + value.ToString();
        }

        public bool PowerOnOff(bool power)
        {
            if (power)
            {
                Console.WriteLine("P_OFF");
                power = !power;
                return power;
            }
            else
            {
                Console.WriteLine("P_ON");
                power = !power;
                return power;
            }
        }

        public void ReceiveData()
        {
            //Тут делаю бесконечный цикл на прием данных и парсю если curr то распарсиваю на свойства если что то другое просто вывожу в статус делов то
        }
    }
}
