using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace spa
{
    class Cell
    {
        public const int TYPE_STRING = 0;
        public const int TYPE_INT = 1;
        public const int TYPE_FLOAT = 2;

        private int _cumulativeWidth;
        private int _width;
        private int _contentType;//0==String; 1==Int; 2==Float
        private String _contents;


        public Cell()
        {
            _cumulativeWidth = 0;
            _width = 0;
            _contents = "";
            setContentType();
        }

        public Cell(String contents)
        {
            if (contents != null)
            {
                _contents = contents;
                _cumulativeWidth = 0;
                this.calculateWidth();
                this.setContentType();
            }
            else
            {
                _contents = "";
                _cumulativeWidth = 0;
                _width = 0;
                _contentType = TYPE_STRING;
            }
        }


        public int cumulativeWidth
        {
            get
            {
                return _cumulativeWidth;
            }
            set
            {
                _cumulativeWidth = value;
            }
        }
        public int width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value >= 0)
                    _width = value;
            }
        }
        public String contents
        {
            get
            {
                if (_contents != null)
                    return _contents;
                return "";
            }
            set
            {
                if (value != null)
                    _contents = value;
            }
        }

        public int contentType
        {
            get
            {
                return _contentType;
            }
            set
            {
                if (value >= 0 && value <= 2)
                    _contentType = value;
            }
        }

        public void setContentType()
        {
            if (_contents != null)
            {
                double contentType = 0;
                if (Double.TryParse(_contents, out contentType))
                {
                    if (contents.Contains("."))
                        _contentType = TYPE_FLOAT;
                    else
                        _contentType = TYPE_INT;
                }
                else _contentType = TYPE_STRING;
            }
        }

        public void calculateWidth()
        {
            if (_contents != null)
                _width = TextRenderer.MeasureText(_contents, Program.FONT).Width;
            else
                _width = 0;
        }

    }
}
