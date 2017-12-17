using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heraldry.Blazon.Structure;
using System.IO;

namespace Heraldry.Rendering.Text
{
    class TextRenderer : CrestRenderer
    {
        public override bool Render(BlazonInstance instance, Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine("Karel.lel");
            }
            
            throw new NotImplementedException();
        }
    }
}
