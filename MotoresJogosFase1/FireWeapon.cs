
namespace MotoresJogosFase1
{
    public class FireWeapon : Command
    {
        public override void Execute(Ship playerShip)
        {
            playerShip.Fire();
            //throw new System.NotImplementedException();
        }
    }
}
