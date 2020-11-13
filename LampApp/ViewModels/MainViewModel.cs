using LampApp.Commands;
using LampApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LampApp.ViewModels
{
    public class MainViewModel : ViewModel
    {

        private Effect effect = new Effect();
        private bool canParseData = false;
        private static int localPort = 8889; 

        UdpClient client = new UdpClient(localPort);
        string data = "";

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

        #region Адрес
        /// <summary>
        /// Поле ввода адреса
        /// </summary>
        private string _Address = "192.168.0.7";
        public string Address
        {
            get => _Address;
            set => Set(ref _Address, value);
        }
        #endregion

        #region Порт
        /// <summary>
        /// Поле ввода порта
        /// </summary>
        private string _Port = "8888";
        public string Port
        {
            get => _Port;
            set => Set(ref _Port, value);
        }
        #endregion

        #region Combobox Эффекты
        private Effect _SelectedEffect;
        public List<Effect> ListEffects { get; set; }
        public Effect SelectedEffect
        {
            get { return _SelectedEffect; }
            set
            {
                Set(ref _SelectedEffect, value);
                canParseData = true;
                IPEndPoint endPointToServer = new IPEndPoint(IPAddress.Parse(Address), int.Parse(Port));
                byte[] mes = Encoding.ASCII.GetBytes("EFF" + SelectedEffect.NumberEffect);
                client.Send(mes, mes.Length, endPointToServer);
            }
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
                Set(ref _Speed, value);
                IPEndPoint endPointToServer = new IPEndPoint(IPAddress.Parse(Address), int.Parse(Port));
                byte[] mes = Encoding.ASCII.GetBytes("SPD" + Speed);
                client.Send(mes, mes.Length, endPointToServer);
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
                Set(ref _Scale, value);
                IPEndPoint endPointToServer = new IPEndPoint(IPAddress.Parse(Address), int.Parse(Port));
                byte[] mes = Encoding.ASCII.GetBytes("SCA" + Scale);
                client.Send(mes, mes.Length, endPointToServer);
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
                Set(ref _Brightness, value);
                IPEndPoint endPointToServer = new IPEndPoint(IPAddress.Parse(Address), int.Parse(Port));
                byte[] mes = Encoding.ASCII.GetBytes("BRI" + Brightness);
                client.Send(mes, mes.Length, endPointToServer);
            }
        }
        #endregion

        #region Power состояние лампы
        private bool _Power = true;
        /// <summary>
        /// Состояние лампы
        /// </summary>
        public bool Power
        {
            get { return _Power; }
            set => Set(ref _Power, value);
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
                    canParseData = true;
                    IPEndPoint endPointToServer = new IPEndPoint(IPAddress.Parse(Address), int.Parse(Port));
                    byte[] mes = Encoding.ASCII.GetBytes("GET");
                    client.Send(mes, mes.Length, endPointToServer);

                });
            }
        }
        #endregion

        #region
        /// <summary>
        /// Бесконечно принимаю данные
        /// </summary>
        /// <param name="res">client.BeginReceive(new AsyncCallback(Recevive), null);</param>
        private void Recevive(IAsyncResult res)
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] recv = client.EndReceive(res, ref endPoint);
                data = $"{Encoding.ASCII.GetString(recv)}";
                Status = data;
                if (data.Contains("CURR") && canParseData)
                {
                    string[] parse = data.Split(' ');
                    foreach (var item in ListEffects)
                    {
                        if (item.NumberEffect == int.Parse(parse[1]))
                            SelectedEffect = item;
                    }
                    Brightness = int.Parse(parse[2]);
                    Speed = int.Parse(parse[3]);
                    Scale = int.Parse(parse[4]);
                    Power = !Convert.ToBoolean(int.Parse(parse[5]));
                    canParseData = false;
                }
                client.BeginReceive(new AsyncCallback(Recevive), null);

            }
            catch (Exception ex)
            {
                Status = ex.Message;
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
                    canParseData = true;
                    string command;
                    if (!Power)
                        command = "P_ON";
                    else
                        command = "P_OFF";
                    IPEndPoint endPointToServer = new IPEndPoint(IPAddress.Parse(Address), int.Parse(Port));
                    byte[] mes = Encoding.ASCII.GetBytes(command);
                    client.Send(mes, mes.Length, endPointToServer);
                    Status = command;
                });
            }
        }
        #endregion

        public MainViewModel()
        {
            canParseData = true;
            ListEffectsModel listEffects = new ListEffectsModel();
            ListEffects = listEffects.Effects;
            client.BeginReceive(new AsyncCallback(Recevive), null);

            SendCommandToGetSettingData();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        Status = "";
                        IPEndPoint endPointToServer = new IPEndPoint(IPAddress.Parse(Address), int.Parse(Port));
                        byte[] mes = Encoding.ASCII.GetBytes("DEB");
                        client.Send(mes, mes.Length, endPointToServer);
                        Task.Delay(2000).Wait();
                    }
                    catch (Exception ex)
                    {

                        Status = ex.Message;
                        Task.Delay(1000).Wait();
                    }
                }

            });

        }

        private void SendCommandToGetSettingData()
        {
            IPEndPoint endPointToServer = new IPEndPoint(IPAddress.Parse(Address), int.Parse(Port));
            byte[] mes = Encoding.ASCII.GetBytes("GET");
            client.Send(mes, mes.Length, endPointToServer);
        }
    }
}
