using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LampApp.Models
{
    public class ListEffectsModel
    {
        public List<Effect> Effects { get; }

        public ListEffectsModel()
        {
            Effects = new List<Effect>
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
