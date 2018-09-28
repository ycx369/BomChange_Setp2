using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BomChange
{
    public partial class Handdone1 : System.Web.UI.Page
    {
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridView1.DataSource = DBHelper.GetDatasByAdapter("select * from handdone").Tables[0];
                GridView1.DataBind();
            }
        }

        #region ECN筛选
        protected void ActionButton_Click(object sender, EventArgs e)
        {
            string ecnstring = Request.Params["FliterTxt"].ToString().Trim();
            ViewState["ecnstring"] = ecnstring;

            if (ecnstring != "")
            {
                Loaddata("where `ECN`= '" + ecnstring + "'");
                GridView1.DataBind();
                ViewState["ecnnum"] = 2;
                //int ecnnum = 1;
                //Response.Write("<script language=javascript>alert('"+ecnnum+"');</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('请输入相应信息！');</script>");
            }

        }
        #endregion

        #region SO筛选
        protected void SO_Click(object sender, EventArgs e)
        {
            string ecnstring = Request.Params["FliterTxt"].ToString().Trim();
            ViewState["ecnstring"] = ecnstring.ToString();

            if (ecnstring != "")
            {
                Loaddata("where `SO`= '" + ecnstring + "'");
                GridView1.DataBind();
                ViewState["ecnnum"] = 3;
                //int ecnnum = 1;
                //Response.Write("<script language=javascript>alert('"+ecnnum+"');</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('请输入相应信息！');</script>");
            }
        }
        #endregion

        #region MO筛选
        protected void MO_Click(object sender, EventArgs e)
        {
            string ecnstring = Request.Params["FliterTxt"].ToString().Trim();
            ViewState["ecnstring"] = ecnstring;

            if (ecnstring != "")
            {
                Loaddata("where `MO`= '" + ecnstring + "'");
                GridView1.DataBind();
                ViewState["ecnnum"] = 4;
            }
            else
            {
                Response.Write("<script language=javascript>alert('请输入相应信息！');</script>");
            }
        }
        #endregion

        #region 物料筛选
        protected void Material_Click(object sender, EventArgs e)
        {
            string ecnstring = Request.Params["FliterTxt"].ToString().Trim();
            ViewState["ecnstring"] = ecnstring;

            if (ecnstring != "")
            {
                Loaddata("where `物料号`= '" + ecnstring + "'");
                GridView1.DataBind();
                ViewState["ecnnum"] = 5;
            }
            else
            {
                Response.Write("<script language=javascript>alert('请输入相应信息！');</script>");
            }
        }
        #endregion


        #region Loaddata查数据
        private void Loaddata(string where)
        {
            GridView1.DataSource = DBHelper.GetDatasByAdapter("SELECT\n" +
            "HANDDONE.id,\n" +
            "HANDDONE.`增加或减少`,\n" +
            "HANDDONE.`柜号`,\n" +
            "HANDDONE.`柜子描述`,\n" +
            "HANDDONE.MO,\n" +
            "HANDDONE.`物料号`,\n" +
            "HANDDONE.`数量`,\n" +
            "HANDDONE.`物料属性`,\n" +
            "HANDDONE.`库位`,\n" +
            "HANDDONE.`变更原因`,\n" +
            "HANDDONE.`总数量`,\n" +
            "HANDDONE.`MO下达车间`,\n" +
            "HANDDONE.`变更影响`,\n" +
            "HANDDONE.`物料描述`,\n" +
            "HANDDONE.ECN,\n" +
            "HANDDONE.SO,\n" +
            "HANDDONE.`申请人`,\n" +
            "HANDDONE.`申请日期`,\n" +
            "HANDDONE.`批准人`,\n" +
            "HANDDONE.`批准日期`,\n" +
            "HANDDONE.`项目名称`,\n" +
            "HANDDONE.`已处理`,\n" +
            "HANDDONE.`处理人员`,\n" +
            "HANDDONE.`完成时间`,\n" +
            "HANDDONE.`收料人`,\n" +
            "HANDDONE.`收料时间`\n" +
            "FROM\n" +
            "HANDDONE\n" + where).Tables[0];
            //"where ISNULL(已处理) or `已处理`=''").Tables[0];

            GridView1.DataBind();

        }

        #endregion


        protected void Button4_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox ckb = (CheckBox)this.GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
                if (ckb.Checked)
                {
                    string huohao = this.GridView1.Rows[i].Cells[1].Text.ToString();
                    string num = DBHelper.ExcuteSqlreturnInt("update ecn1 SET `已处理`='',`处理人员`='',`完成时间`=Null  where id='" + huohao + "'").ToString();
    
                }

            }
            GridView1.DataSource = DBHelper.GetDatasByAdapter("select * from ecn1").Tables[0];

            GridView1.DataBind();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("BomChange1.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {


            #region 方法2

            //GridView1.PageIndex = e.NewPageIndex;
            //InitGridView();

            #endregion

            #region 方法3


            // 得到该控件
            GridView theGrid = sender as GridView;
            int newPageIndex = 0;
            if (e.NewPageIndex == -3)
            {
                //点击了Go按钮
                TextBox txtNewPageIndex = null;

                //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
                GridViewRow pagerRow = theGrid.BottomPagerRow;

                if (pagerRow != null)
                {
                    //得到text控件
                    txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
                }
                if (txtNewPageIndex != null)
                {
                    //得到索引
                    newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
                }
            }
            else
            {
                //点击了其他的按钮
                newPageIndex = e.NewPageIndex;
            }
            //防止新索引溢出
            newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
            newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;

            //得到新的值
            theGrid.PageIndex = newPageIndex;

            //重新绑定
            GridView1.DataSource = DBHelper.GetDatasByAdapter("select * from handdone").Tables[0];
            GridView1.DataBind();
            #endregion

         


        }

 


    }
}