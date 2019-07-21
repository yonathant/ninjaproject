using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void login_Click(object sender, EventArgs e)
    {
        string username = usernameTB.Text;
        string password = passwordTB.Text;

        if((usernameTB.Text == "ninjaboss") && (passwordTB.Text == "ninja123"))
        {
            Response.Redirect("MyGames.aspx");
        }
        else
        {
            wrongLoginTxt.Text = "שם משתמש או סיסמא לא נכונים";
        }
    }
}