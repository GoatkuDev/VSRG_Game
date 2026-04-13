using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhythm
{
    internal class Note
    {
        public float x;
        public float y;
        public int Column;
        public int HitPoint;
        public Image Image;
    

    public Note(float x, float y, int Column, int HitPoint, Image Image)
        {
            this.x = x;
            this.y = y;
            this.Column = Column;
            this.HitPoint = HitPoint;
            this.Image = Image;
        }
    }
}