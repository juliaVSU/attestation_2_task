using System;
using System.Collections.Generic;
using System.Drawing;
using attestation_2_3_iiasp.Garden_.Etc;

namespace attestation_2_3_iiasp.Garden_
{
    public class GardenDrawable
    {
        private static readonly SizeF CellSize = new SizeF(100, 100);

        private readonly Garden _garden;

        private readonly IGardenCellDrawable[,] _drawables;

        private readonly List<IGardenCellDrawable> _drawablesForDraw = new List<IGardenCellDrawable>();

        public Graphics Graphics { get; set; }

        public Size Size
        {
            get
            {
                return new Size((int)(CellSize.Width * _garden.XLen), (int)(CellSize.Height * _garden.YLen));
            }
        }

        private int Xn { get { return _garden.XLen; } }
        private int Yn { get { return _garden.YLen; } }


        public GardenDrawable(Graphics graphics, Garden garden)
        {
            Graphics = graphics;
            _garden = garden;
            int
                xn = _garden.XLen,
                yn = _garden.YLen;
            _drawables = new IGardenCellDrawable[xn, yn];
            for (int x = 0; x < _garden.XLen; x++)
                for (int y = 0; y < _garden.YLen; y++)
                    CreateDrawable(_garden[x, y]);
            _garden.Replaced += CreateDrawable;
        }

        private void CreateDrawable(IGardenCell cell)
        {
            var drawable = DrawableFactory.Create(cell, this);
            drawable.Changed += drawable_Changed;
            _drawables[cell.X, cell.Y] = drawable;
            _drawablesForDraw.Add(drawable);
        }

        private void drawable_Changed(IGardenCellDrawable drawable)
        {
            if (!_drawablesForDraw.Contains(drawable))
                _drawablesForDraw.Add(drawable);
        }


        public void Draw()
        {
            foreach (var drawable in _drawablesForDraw)
            {
                Graphics.FillRectangle(Brushes.White, GetDstRect(drawable.X, drawable.Y));
                drawable.Draw();
            }
            _drawablesForDraw.Clear();
        }

        protected IGardenCellDrawable this[int x, int y]
        {
            get { return _drawables[x, y]; }
        }

        public RectangleF GetDstRect(int x, int y)
        {
            return new RectangleF(new PointF(x * CellSize.Width, y * CellSize.Height), CellSize);
        }

        public void InvalidateAll()
        {
            _drawablesForDraw.Clear();
            int
                xn = Xn,
                yn = Yn;
            for (int x = 0; x < xn; x++)
                for (int y = 0; y < yn; y++)
                    _drawablesForDraw.Add(this[x, y]);
        }
    }

    public class DrawableFactory
    {
        public static IGardenCellDrawable Create(IGardenCell gardenCell, GardenDrawable garden)
        {
            var p = gardenCell as Plant;
            if (p != null)
                return new PlantDrawable(p, garden);
            var t = gardenCell as Tile;
            if (t != null)
                return new TileDrawable(t, garden);
            var e = gardenCell as Empty;
            if (e != null)
                return new EmptyDrawable(e);
            throw new Exception("unknown garden cell");
        }
    }

    public interface IGardenCellDrawable
    {
        int X { get; }
        int Y { get; }
        void Draw();
        event Action<IGardenCellDrawable> Changed;
    }



    public abstract class GardenCellDrawable : IGardenCellDrawable
    {
        private readonly GardenDrawable _garden;

        private readonly IGardenCell _cell;

        protected GardenCellDrawable(GardenDrawable garden, IGardenCell cell)
        {
            _garden = garden;
            _cell = cell;
        }

        public int Y
        {
            get { return _cell.Y; }
        }

        public int X
        {
            get { return _cell.X; }
        }

        public GardenDrawable GardenDrawable
        {
            get { return _garden; }
        }

        protected void RaiseChanged()
        {
            var ev = Changed;
            if (ev != null)
                ev(this);
        }

        public abstract void Draw();

        public event Action<IGardenCellDrawable> Changed;
    }

    public class PlantDrawable : GardenCellDrawable
    {
        private readonly Plant _plant;

        public PlantDrawable(Plant plant, GardenDrawable garden)
            : base(garden, plant)
        {
            _plant = plant;
            _plant.RipenessChanged += RaiseChanged;
        }

        public override void Draw()
        {
            Images.Plant.Draw(GardenDrawable.Graphics, GardenDrawable.GetDstRect(X, Y), _plant.RipenessStage, _plant.Type);
        }

        public static void Next(ref int ripeness)
        {
            if (ripeness == Images.Plant.RipenessCount)
                ripeness = 0;
            else
                ripeness++;
        }


        public static void ValidateType(int type)
        {
            Helper.ValidateBelong(type, 0, 2);
        }
    }

    public class TileDrawable : GardenCellDrawable
    {
        private readonly Tile _tile;

        public TileDrawable(Tile tile, GardenDrawable garden)
            : base(garden, tile)
        {
            _tile = tile;
            _tile.Changed += RaiseChanged;
        }

        public override void Draw()
        {
            var dstRect = GardenDrawable.GetDstRect(X, Y);
            GardenDrawable.Graphics.DrawImage(TileImage, dstRect, Helper.ToRectangleF(TileImage.Size), GraphicsUnit.Pixel);
            if (_tile.HasMario)
                MarioImage.Draw(GardenDrawable.Graphics, Helper.DecreaseBorder(dstRect, 10));
        }

        private static Image TileImage { get { return Garden_Resources.tile; } }
    }

    public class MarioImage
    {
        public static void Draw(Graphics graphics, RectangleF rect)
        {
            graphics.DrawImage(Garden_Resources.mario, rect);
        }
    }


    public class EmptyDrawable : IGardenCellDrawable
    {
        private readonly Empty _empty;

        public EmptyDrawable(Empty empty)
        {
            _empty = empty;
        }

        public int Y { get { return _empty.Y; } }
        public void Draw()
        {

        }

        public event Action<IGardenCellDrawable> Changed;
        public int X { get { return _empty.X; } }
    }


}
