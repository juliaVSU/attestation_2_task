using System.Windows.Forms;

namespace attestation_2_3_iiasp.Garden_
{
    internal class GardenUserInteraction
    {
        private readonly Garden _garden;
        private readonly GardenDrawable _drawable;

        public GardenUserInteraction(Garden garden, GardenDrawable drawable)
        {
            _garden = garden;
            _drawable = drawable;
        }

        public void ProcessKeyDown(KeyEventArgs arg)
        {
            switch (arg.KeyCode)
            {
                case Keys.Up:
                    _garden.Mario.Move(0, -1); break;
                case Keys.Down:
                    _garden.Mario.Move(0, +1); break;
                case Keys.Left:
                    _garden.Mario.Move(-1, 0); break;
                case Keys.Right:
                    _garden.Mario.Move(+1, 0); break;
            }
        }
    }
}
