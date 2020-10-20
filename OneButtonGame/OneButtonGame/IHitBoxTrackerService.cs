using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneButtonGame
{
    interface IHitBoxTrackerService
    {
        List<HitBox> WorldHitBoxes { get; }

        void ClearHitBoxes();
        void RegisterHitBoxes(List<HitBox> boxes);

    }
}
