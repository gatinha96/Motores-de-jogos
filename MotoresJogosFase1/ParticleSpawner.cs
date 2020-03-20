using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresJogosFase1
{
    public class ParticleSpawner
    {
        List<Observer> observers = new List<Observer>();

        public void Notify()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].OnNotify();
            }
        }

        public void AddObserver(Observer observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(Observer observer)
        {
            observers.Remove(observer);
        }
    }





    public abstract class Observer
    {
        public void OnNotify() {
            MessageBus.InsertNewMessage(new ConsoleMessage("???"));
        }
    }


    /*public class SpawnParticle : Observer
    {

        Ship ship;
        Vector3 position;

        public SpawnParticle(Ship ship, Vector3 position)
        {
            this.ship = ship;
            this.position = position;
        }

        public override void OnNotify()
        {
            throw new NotImplementedException();
        }
    }*/
}
