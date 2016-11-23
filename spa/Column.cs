using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace spa
{
    class Column
    {

        private int _width;
        private int _cumulativeOffset;
        private int _type;
        private bool _rearranged;
        private ArrayList _cells;

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

        public int cumulativeWidth
        {
            get
            {
                return _cumulativeOffset;
            }
            set
            {
                _cumulativeOffset = value;
            }
        }



        public int type
        {
            get { return _type; }
            set { _type = value; }
        }



        public bool rearranged
        {
            get { return _rearranged; }
            set { _rearranged = value; }
        }


        public ArrayList cells
        {
            get
            {
                if (_cells == null)
                    _cells = new ArrayList();
                return _cells;
            }
            set
            {
                if (value != null && value.Count > 0)

                    _cells = value;
            }
        }

        public Boolean addNewCell(String contents)
        {
            if (contents != null)
            {
                _cells.Add(contents);
                int curWidth = calculateTextWidth(contents);
                if (curWidth > _width)
                {
                    _cumulativeOffset += (curWidth - _width);
                    _width = curWidth;
                }

                return true;
            }
            return false;
        }

        public void addCell(Cell theCell)
        {
            if (theCell != null)
            {
                _cells.Add(theCell);
            }
        }

        public int calculateTextWidth(String contents)
        {
            if (contents == null || contents.Length < 1)
                return 0;
            return TextRenderer.MeasureText(contents, Program.FONT).Width;
        }


        public void calculateWidth()
        {
            if (_cells != null)
            {
                foreach (String cellContents in _cells)
                {
                    if (cellContents != null)
                    {
                        int curWidth = calculateTextWidth(cellContents);
                        if (curWidth > _width)
                            _width = curWidth;
                    }
                }
            }
            _width = 0;
        }

        public Column()
        {
            _cells = new ArrayList();
            _cumulativeOffset = 0;
            _width = 0;
            _type = Cell.TYPE_STRING;
            _rearranged = false;
        }

    }
}
