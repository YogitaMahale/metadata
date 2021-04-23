using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI.WebControls;




partial class ProgressBar : System.Web.UI.UserControl
{

    private System.Drawing.Color _colFillColor;
    private System.Drawing.Color _colBackcolor;

    private System.Drawing.Color _colBorderColor = System.Drawing.Color.Black;
    private int _intBorder = 1;
    private int _intCellspacing = 1;
    private int _intCellpadding = 1;
    private int _intHeight = 15;

    private int _intWidth = 100;
    private int _intBlockNumber = 5;
    private int _intValue;

    private TableRow _tblBlock;
    public System.Drawing.Color BGColor
    {
        get { return _colBackcolor; }
        set { _colBackcolor = value; }

    }
    public System.Drawing.Color FillColor
    {
        get { return _colFillColor; }
        set { _colFillColor = value; }
    }
    public System.Drawing.Color BorderColor
    {
        get { return _colBorderColor; }
        set { _colBorderColor = value; }
    }
    public int BorderSize
    {
        get { return _intBorder; }
        set { _intBorder = value; }
    }
    public int Cellpadding
    {
        get { return _intCellpadding; }
        set { _intCellpadding = value; }
    }
    public int CellSpacing
    {
        get { return _intCellspacing; }
        set { _intCellspacing = value; }
    }
    public int Blocks
    {
        get { return _intBlockNumber; }
        set { _intBlockNumber = value; }
    }

    public int Value
    {
        get { return _intValue; }
        set { _intValue = value; }
    }
    public int Height
    {
        get { return _intHeight; }
        set { _intHeight = value; }
    }
    public int Width
    {
        get { return _intWidth; }
        set { _intWidth = value; }
    }
    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        int intBlocks = 0;

        // add a new row to the table
        
        _tblBlock = new TableRow();
        // create cells and add to the row
        for (intBlocks = 1; intBlocks <= this.Blocks; intBlocks++)
        {
            TableCell tblCell = new TableCell();
            tblCell.Text = " ";
                    
            if (intBlocks <= Math.Ceiling( Convert.ToDouble( (this.Value * this.Blocks / 100))))
            {
                tblCell.BackColor = this.FillColor;
            }
            _tblBlock.Cells.Add(tblCell);
        }
        tblProgressBar.Rows.Add(_tblBlock);

      
        //set the progress bar properties
        tblProgressBar.CellPadding = this.Cellpadding;
        tblProgressBar.CellSpacing = this.CellSpacing;
        tblProgressBar.Width = this.Width;
        tblProgressBar.Height = this.Height;
        tblProgressBar.BackColor = this.BGColor;
        tblProgressBar.BorderColor = this.BorderColor;

    }
    public ProgressBar()
    {
        PreRender += Page_PreRender;
    }
}
