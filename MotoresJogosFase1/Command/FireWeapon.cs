using Microsoft.Xna.Framework;

namespace MotoresJogosFase1
{
    public class FireWeapon : Command
    {
        Ship s;
        ParticleSpawner particleSpawner = new ParticleSpawner();

        public FireWeapon(Ship s)
        {
            this.s = s;
        }

        public override void Execute()
        {
            MessageBus.Messages.Add(new ConsoleMessage("Firing!"));
            particleSpawner.Notify();
            s.Fire();
        }
    }
}
