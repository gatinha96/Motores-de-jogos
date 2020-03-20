using Microsoft.Xna.Framework;

namespace MotoresJogosFase1
{
    public class FireWeapon : Command
    {
        Ship s;

        public FireWeapon(Ship s)
        {
            this.s = s;
        }

        public override void Execute()
        {
            MessageBus.Messages.Add(new ConsoleMessage("Firing!"));
            ParticleSpawner particleSpawner = new ParticleSpawner();
            s.Fire();
        }
    }
}
