using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneButtonGame
{
    public sealed class HitBoxTrackerService : IHitBoxTrackerService
    {
        public List<HitBox> WorldHitBoxes { get; set; }

        private Game _game;

        public HitBoxTrackerService(Game game)
        {
            _game = game;
            ClearHitBoxes();
        }



        public void ClearHitBoxes()
        {
            WorldHitBoxes = new List<HitBox>();
        }
        public void RegisterHitBoxes(List<HitBox> boxes)
        {
            foreach (HitBox box in boxes)
            {
                box.TrackingID = WorldHitBoxes.Count + 1;
                WorldHitBoxes.Add(box);
            }
        }
        public void RegisterHitBox(HitBox box)
        {
            box.TrackingID = WorldHitBoxes.Count + 1;
            WorldHitBoxes.Add(box);
        }
    }
}
