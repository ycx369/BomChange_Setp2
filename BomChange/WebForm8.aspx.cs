using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BomChange
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = DBhelper2.GetDatasByAdapter("select * from test2").Tables[0];
            GridView1.DataBind();
            this.GridView1.Rows[0].Cells[0].Text = "testtt";
            this.GridView1.Rows[1].Cells[0].Text = "testtt";
            this.GridView1.Rows[2].Cells[0].Text = "testtt";
            this.GridView1.Rows[3].Cells[0].Text = "testtt";
            this.GridView1.Rows[4].Cells[0].Text = "testtt";
            this.GridView1.Rows[5].Cells[0].Text = "testtt";
            this.GridView1.Rows[6].Cells[0].Text = "testtt";
        }
    }
}