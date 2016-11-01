using System;
using System.Windows.Forms;
using attestation_2_3_iiasp.Garden_.Etc;

namespace attestation_2_3_iiasp.Garden_
{
    public partial class GardenForm : Form
    {
        private Garden _garden;

        private GardenDrawable _drawable;

        private GardenUserInteraction _interaction;

        public GardenForm()
        {
            FirstInvokeDUpdater = true;
            InitializeComponent();
        }

        private void GardenForm_Paint(object sender, PaintEventArgs e)
        {
            _drawable.Draw();
        }

        private void GardenForm_Load(object sender, EventArgs e)
        {
            _garden = Garden.Load();
            _drawable = new GardenDrawable(pictureBox_Garden.CreateGraphics(), _garden);
            _interaction = new GardenUserInteraction(_garden, _drawable);

            var size = _drawable.Size;
            ClientSize = Helper.AddHeight(size, splitContainer1.Panel1.Size.Height);
            pictureBox_Garden.Size = size;

            _drawable.Graphics = pictureBox_Garden.CreateGraphics();

            var gardenUpdter = new Timer();
            gardenUpdter.Tick += garden_Updater;
            gardenUpdter.Interval = 500;
            gardenUpdter.Enabled = true;

            var drawableUpdater = new Timer();
            drawableUpdater.Tick += drawable_Updater;
            drawableUpdater.Interval = 50;
            drawableUpdater.Enabled = true;
        }

        private void drawable_Updater(object sender, EventArgs e)
        {
            if (FirstInvokeDUpdater)
            {
                _drawable.InvalidateAll();
                FirstInvokeDUpdater = false;
            }
            _drawable.Draw();
        }

        private bool FirstInvokeDUpdater { get; set; }

        private void garden_Updater(object sender, EventArgs e)
        {
            _garden.Update();
        }

        private void GardenForm_KeyDown(object sender, KeyEventArgs e)
        {
            _interaction.ProcessKeyDown(e);
        }

        private void pictureBox_Garden_SizeChanged(object sender, EventArgs e)
        {
            _drawable.InvalidateAll();
        }
    }
}
