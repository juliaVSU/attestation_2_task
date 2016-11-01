using System;
using System.IO;
using System.Linq;
using attestation_2_3_iiasp.Garden_.Etc;

namespace attestation_2_3_iiasp.Garden_
{
    public class Garden
    {
        private readonly Mario _mario = new Mario();

        private readonly IGardenCell[,] _grid; // todo grid of cell

        private readonly int _xn;

        private readonly int _yn;

        public DateTime LastUpdateTime { get; private set; }

        public DateTime NowUpdateTime { get; private set; }

        public TimeSpan UpdateDelta { get; private set; }

        public int XLen { get { return _xn; } }

        public int YLen { get { return _yn; } }
        public Mario Mario { get { return _mario; } }

        public IGardenCell this[int x, int y] // todo to cell
        {
            get { return _grid[x, y]; }
            set
            {
                _grid[x, y] = value;
                Helper.Invoke(Replaced, value);
                var ec = value as IGardenCellExhausted;
                if (ec != null)
                    ec.Exhausted += ec_Exhausted;
                if (!_mario.InGarden && value.GetType() == typeof(Tile))
                    _mario.Tile = (Tile)value;
            }
        }

        private void ec_Exhausted(IGardenCellExhausted cell)
        {
            cell.Exhausted -= ec_Exhausted;
            var x = cell.X;
            var y = cell.Y;
            this[x, y] = CellFactory.Replace(cell, this);
        }

        public Garden(int xn, int yn)
        {
            _xn = xn;
            _yn = yn;
            _grid = new IGardenCell[xn, yn];
            LastUpdateTime = NowUpdateTime = DateTime.Now;
        }

        public void Update()
        {
            SetDeltaTime();
            for (int x = 0; x < XLen; x++)
                for (int y = 0; y < YLen; y++)
                    this[x, y].Update();
        }

        private void SetDeltaTime()
        {
            LastUpdateTime = NowUpdateTime;
            NowUpdateTime = DateTime.Now;
            UpdateDelta = NowUpdateTime - LastUpdateTime;
        }

        public static Garden Load()
        {
            var lines = File.ReadLines("garden.txt").ToArray();
            var whLine = lines[0].Split(' ');
            var garden = new Garden(int.Parse(whLine[1]), int.Parse(whLine[0]));
            int x, y = 0;
            foreach (var yLine in lines.Skip(1))
            {
                x = 0;
                foreach (var xyLine in yLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    garden[x, y] = CellFactory.Create(xyLine, garden, x, y);
                    x++;
                }
                y++;
            }
            return garden;
        }

        public IGardenCell SafeGet(int x, int y)
        {
            if (Contains(x, y))
                return this[x, y];
            return null;
        }

        private bool Contains(int x, int y)
        {
            return
                Helper.Belong(x, 0, XLen - 1) &&
                Helper.Belong(y, 0, YLen - 1);
        }

        public event Action<IGardenCell> Replaced;
    }

    public class Mario
    {
        private Tile _tile;

        public Tile Tile
        {
            get { return _tile; }
            set
            {
                var oldTile = _tile;
                _tile = value;
                Helper.Invoke(TileChanged, oldTile, _tile);
            }
        }

        public bool InGarden { get { return Tile != null; } }

        public void Move(int dx, int dy)
        {
            if (Tile == null)
                return;
            var adjacentTile = Tile.GetAdjacentTile(dx, dy);
            if (adjacentTile != null)
                Tile = adjacentTile;
        }

        public event Action<Tile, Tile> TileChanged;
    }

    public class CellFactory
    {
        public static IGardenCell Create(string cellType, Garden garden, int x, int y)
        {
            switch (cellType)
            {
                case "t":
                    return new Tile(garden, x, y);
                case "p0":
                    return new Plant(garden, x, y, 0);
                case "p1":
                    return new Plant(garden, x, y, 1);
                case "p3":
                    return new Plant(garden, x, y, 2);
                case "x":
                    return new Empty(x, y);
            }
            throw new Exception("unknown graden cell");
        }

        public static IGardenCell Replace(IGardenCell cell, Garden garden)
        {
            var p = cell as Plant;
            if (p != null)
                return new Plant(garden, cell.X, cell.Y, p.Type);
            throw new Exception();
        }
    }



    public interface IGardenCell
    {
        int X { get; }
        int Y { get; }
        void Update();
    }

    public interface IGardenCellExhausted : IGardenCell
    {
        event Action<IGardenCellExhausted> Exhausted;
    }

    public abstract class GardenCell : IGardenCell
    {
        protected GardenCell(Garden garden, int x, int y)
        {
            Garden = garden;
            Y = y;
            X = x;
        }

        public Garden Garden { get; private set; }
        public int Y { get; private set; }
        public int X { get; private set; }
        public abstract void Update();

        protected TimeSpan UpdateTimeDelta { get { return Garden.UpdateDelta; } }
    }



    public enum PlantOldness
    {
        Young = 0,
        Ripe,
        Old
    }


    public class Plant : GardenCell, IGardenCellExhausted
    {
        private readonly int _type;

        /// <summary>
        /// 0 to 100
        /// </summary>
        private double _ripeness;

        private double RipenessUpdateDelta
        {
            get { return UpdateTimeDelta.TotalSeconds * 30; }
        }

        public int Type { get { return _type; } }

        public int Helath { get; set; }

        public int RipenessStage
        {
            get { return (int)(Ripeness / 17); }
        }

        public PlantOldness Oldness
        {
            get
            {
                var r = RipenessInt;
                if (Helper.Belong(r, 0, 40))
                    return PlantOldness.Young;
                if (Helper.Belong(r, 40, 70))
                    return PlantOldness.Ripe;
                return PlantOldness.Old;
            }
        }

        public bool IsDied { get { return Ripeness >= 100; } }

        public double Ripeness
        {
            get { return _ripeness; }
            private set
            {
                if (IsDied)
                    return;
                _ripeness = value;
                if (IsDied)
                    Helper.Invoke(Exhausted, this);
                else
                    Helper.Invoke(RipenessChanged);
            }
        }

        public int RipenessInt { get { return (int)Ripeness; } }

        public Plant(Garden garden, int x, int y, int type)
            : base(garden, x, y)
        {
            PlantDrawable.ValidateType(type);
            _type = type;
        }

        public override void Update()
        {
            Ripeness += RipenessUpdateDelta;
        }



        public event Action RipenessChanged;

        public IGardenCell Cell { get { return this; } }
        public event Action<IGardenCellExhausted> Exhausted;
    }







    public class Tile : GardenCell
    {
        public Tile(Garden garden, int x, int y)
            : base(garden, x, y)
        {
            Garden.Mario.TileChanged += Mario_TileChanged;
        }

        void Mario_TileChanged(Tile arg1, Tile arg2)
        {
            if (arg1 == this || arg2 == this)
                Helper.Invoke(Changed);
        }

        public override void Update() { }

        public bool HasMario { get { return Garden.Mario.Tile == this; } }

        public Tile GetAdjacentTile(int dx, int dy)
        {
            if (Math.Abs(dx) + Math.Abs(dy) == 1)
                return Garden.SafeGet(X + dx, Y + dy) as Tile;
            return null;
        }

        public event Action Changed;
    }

    public class Empty : IGardenCell
    {
        public Empty(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public void Update() { }
    }
}
