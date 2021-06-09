using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace tesst
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            load();
        }
        void HT()
        {
            if (Session["MaTV"] == null)
            {
                MultiView1.ActiveViewIndex = 0;
            }
            else
            {
                MultiView1.ActiveViewIndex = 1;
                Label5.Text = Session["MaTV"].ToString();
                Label7.Text = ((DateTime)Session["ThoiDiem"]).ToString("ddMM-yyyy HH:mm:ss");
                Image1.ImageUrl = "~/tesst/" + Session["Hinh"].ToString();
            }
        }
        public void load()
        {
            if (!Page.IsPostBack)
            {
                if ( Request.Cookies["MaTV"] != null)
                {
                    if (Request.Cookies["MK"] != null)
                    {
                        // Hiển thị thông tin đăng nhập sẵn trong form đăng
                        txt_tenDN.Text = Request.Cookies["MaTV"].Value;
                        txt_MK.Text = Request.Cookies["MK"].Value;
                        // Đánh dấu mục ghi nhớ tài khoản
                        CheckBox1.Checked = true;
                    }
                }
            }
            this.HT();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txt_MK.Text.Length < 6)
            {
                Label3.Text = "Mật khẩu có ít nhất có 6 kí tự";
            }
            else
            {
                HttpCookie ckiMaTV = new HttpCookie("MaTV", txt_tenDN.Text);
                HttpCookie ckiMK = new HttpCookie("MK", txt_MK.Text);
                if (CheckBox1.Checked)
                {
                    ckiMaTV.Expires = DateTime.Now.AddMonths(1);
                    ckiMK.Expires = DateTime.Now.AddMonths(1);
                    Label3.Text = "Tài khoản đã đc ghi nhớ";
                    Session["MaTV"] = txt_tenDN.Text;
                    Session["ThoiDiem"] = DateTime.Now;
                    Session["Hinh"] = "mm1.gif";
                }
                else
                {
                    ckiMaTV.Expires = DateTime.Now.AddMonths(-1);
                    ckiMK.Expires = DateTime.Now.AddMonths(-1);
                    Label3.Text = "Tài khoản đã thoát";
                }
                Response.Cookies.Add(ckiMaTV);
                Response.Cookies.Add(ckiMK);
            }
            this.HT();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("MaTV");
            Session.Remove("MK");
        }
    }
}