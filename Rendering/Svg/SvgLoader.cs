using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Svg;

namespace Heraldry.Rendering.Svg {


    class SvgLoader {

        private int Height { get; set; }
        private int Width { get; set; }

        private string SvgDirectory {
            get {
                return Environment.CurrentDirectory + "\\resources\\rendering\\";
            }
        }

        public int Loaded { get; private set; } = 0;

        public SvgLoader(int heigth, int width) {
            this.Height = heigth;
            this.Width = width;
        }

        public SvgDocument GetSvgFromFile(string name) {
            SvgDocument retval = null;
            if (File.Exists(SvgDirectory + name)) {
                retval = SvgDocument.Open(SvgDirectory + name);
                Loaded++;
            } else {
                throw new FileNotFoundException("File " + SvgDirectory + " " + name + " is missing.");
                //TODO: might not be mandatory to kill the app here
            }
            return AdjustSize(retval);
        }

        /// <summary>
        /// Check aspect ratio and adjust size.
        /// TODO: check more picture properties
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Picture with adjusted maximum size with same aspect ratio as input.</returns>
        private SvgDocument AdjustSize(SvgDocument document) {
            if (document.Height > this.Height) {
                document.Width = (int)((document.Width / (double)document.Height) * this.Height);
                document.Height = this.Height;
            }
            return document;
        }
    }
}
