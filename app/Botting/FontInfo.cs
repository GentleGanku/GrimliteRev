using System;
using System.Drawing;

namespace Grimoire.Botting
{
    public class FontInfo
    {
        #region Fields

        // Heights and positions in pixels.
        public float EmHeightPixels;

        public float AscentPixels;
        public float DescentPixels;
        public float CellHeightPixels;
        public float InternalLeadingPixels;
        public float LineSpacingPixels;
        public float ExternalLeadingPixels;

        // Distances from the top of the cell in pixels.
        public float RelTop;

        public float RelBaseline;
        public float RelBottom;

        #endregion

        #region Constructors

        public FontInfo(Graphics gr, Font the_font)
        {
            float em_height = the_font.FontFamily.GetEmHeight(the_font.Style);
            EmHeightPixels = ConvertUnits(gr, the_font.Size, the_font.Unit, GraphicsUnit.Pixel);
            float design_to_pixels = EmHeightPixels / em_height;
            AscentPixels = design_to_pixels * the_font.FontFamily.GetCellAscent(the_font.Style);
            DescentPixels = design_to_pixels * the_font.FontFamily.GetCellDescent(the_font.Style);
            CellHeightPixels = AscentPixels + DescentPixels;
            InternalLeadingPixels = CellHeightPixels - EmHeightPixels;
            LineSpacingPixels = design_to_pixels * the_font.FontFamily.GetLineSpacing(the_font.Style);
            ExternalLeadingPixels = LineSpacingPixels - CellHeightPixels;
            RelTop = InternalLeadingPixels;
            RelBaseline = AscentPixels;
            RelBottom = CellHeightPixels;
        }

        #endregion

        #region Methods

        private float ConvertUnits(Graphics gr, float value, GraphicsUnit from_unit, GraphicsUnit to_unit)
        {
            if (from_unit == to_unit) return value;

            switch (from_unit)
            {
                case GraphicsUnit.Document:
                    value *= gr.DpiX / 300;
                    break;
                case GraphicsUnit.Inch:
                    value *= gr.DpiX;
                    break;
                case GraphicsUnit.Millimeter:
                    value *= gr.DpiX / 25.4F;
                    break;
                case GraphicsUnit.Pixel:
                    break;
                case GraphicsUnit.Point:
                    value *= gr.DpiX / 72;
                    break;
                default:
                    throw new Exception("Unknown input unit " +
                        from_unit.ToString() + " in FontInfo.ConvertUnits");
            }

            switch (to_unit)
            {
                case GraphicsUnit.Document:
                    value /= gr.DpiX / 300;
                    break;
                case GraphicsUnit.Inch:
                    value /= gr.DpiX;
                    break;
                case GraphicsUnit.Millimeter:
                    value /= gr.DpiX / 25.4F;
                    break;
                case GraphicsUnit.Pixel:
                    break;
                case GraphicsUnit.Point:
                    value /= gr.DpiX / 72;
                    break;
                default:
                    throw new Exception("Unknown output unit " +
                        to_unit.ToString() + " in FontInfo.ConvertUnits");
            }

            return value;
        }

        #endregion
    }
}
