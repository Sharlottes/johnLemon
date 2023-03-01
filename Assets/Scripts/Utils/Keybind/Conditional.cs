using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utils.Keybind
{
    public delegate bool Condit();
    public delegate bool Condit<T>(out T res);

    public abstract class Conditional
    {
        public Conditional(Condit condition) => this.condition = condition;
        public Condit condition;
    }
    public abstract class Conditional<T> : Conditional
    {
        public Conditional(Condit<T> condition) : base(() => condition(out T res)) => this.condition = condition;
        public new Condit<T> condition;
    }
}