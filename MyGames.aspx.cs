using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


public partial class MyGames : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["savePop"] == "true")
            saveConfirmPop.Attributes.CssStyle.Add("display", "block");
            savePop.Attributes.CssStyle.Add("display", "block");
    } 

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

        // טעינה של העץ
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();

        //הקפצה של מונה האי די בתוך קובץ האקס אם אל באחד
        int myId = Convert.ToInt16(xmlDoc.SelectSingleNode("//idCounter").InnerXml);
        myId++;
        string myNewId = myId.ToString();
        xmlDoc.SelectSingleNode("//idCounter").InnerXml = myNewId;

        // יצירת ענף משחק     
        XmlElement myNewGameNode = xmlDoc.CreateElement("game");
        myNewGameNode.SetAttribute("gid", myNewId);
        myNewGameNode.SetAttribute("gameCode", myNewId);
        myNewGameNode.SetAttribute("publish", "false");
        myNewGameNode.SetAttribute("quesTime", "20");
        myNewGameNode.SetAttribute("quesNum", "0");

        XmlElement myNewNameNode = xmlDoc.CreateElement("gameName");
        myNewNameNode.InnerXml = TextBox1.Text;
        myNewNameNode.InnerXml = Server.UrlDecode(TextBox1.Text);
        myNewGameNode.AppendChild(myNewNameNode);


        XmlNode FirstGame = xmlDoc.SelectNodes("/games/game").Item(0);
        xmlDoc.SelectSingleNode("/games").InsertBefore(myNewGameNode, FirstGame);
        XmlDataSource1.Save();
        GridView1.DataBind();
        TextBox1.Text = " ";
        addGameBtn.Enabled = false;
    }

    public string CheckIfCanPublish(string gid)
    {
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
        XmlNode myGame = xmlDoc.SelectSingleNode("/games/game[@gid=" + gid + "]");

        bool canPublish = false;
        string thisClass = "";

        if (myGame.SelectNodes("question").Count >= 10)
        {
            canPublish = true;
            thisClass = "enabled";
        }
        if (canPublish == false)
        {
            myGame.Attributes["publish"].InnerText = "False";
            XmlDataSource1.Save();
            thisClass = "disabled";
        }
        return thisClass;
    }

    protected void isPassCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();

        CheckBox myCheckBox = (CheckBox)sender;
        // מושכים את האי די של הפריט באמצעות המאפיין שהוספנו באופן ידני לתיבה
        string theId = myCheckBox.Attributes["theItemId"];


        XmlNode theGames = xmlDoc.SelectSingleNode("/games/game[@gid=" + theId + "]");

        //קבלת הערך החדש של התיבה לאחר הלחיצה
        bool NewIsPass = myCheckBox.Checked;

        //עדכון של המאפיין בעץ
        theGames.Attributes["publish"].InnerText = NewIsPass.ToString();


        XmlDataSource1.Save();
        GridView1.DataBind();


    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
        ImageButton i = (ImageButton)e.CommandSource;
        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string theId = i.Attributes["theItemId"];
        Session["theItemIdSession"] = i.Attributes["theItemId"];


        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":

                deleteRow(theId);
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editRow":
                Response.Redirect("Edit.aspx");
                break;
        }
    }

    //מחיקת משחק
    public void deleteRow(string theItemId)
    {
        popUpWindow.Attributes.CssStyle.Add("display", "block");
    
        popUp.Attributes.CssStyle.Add("display", "block");

    }
    

    protected void ExitBtn_Click(object sender,   EventArgs e)
    {
        popUpWindow.Attributes.CssStyle.Add("display", "none");
        popUp.Attributes.CssStyle.Add("display", "none");
    }

    protected void DeleteFinal_Click(object sender, EventArgs e)
    {
        Button myDeleteBtn = (Button)sender;
        string ButtonID = myDeleteBtn.ID;
        string PopUpID = ButtonID.Substring(4);
        popUpWindow.Attributes.CssStyle.Add("display", "none");
        popUpWindow.Attributes.CssStyle.Add("display", "none");

        string theItemId = Session["theItemIdSession"].ToString();
        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        XmlNode node = Document.SelectSingleNode("/games/game[@gid=" + theItemId + "]");
        node.ParentNode.RemoveChild(node);
        XmlDataSource1.Save();
        GridView1.DataBind();
    }
    protected void saveConfBtn_btn(object sender, EventArgs e)
    {
        saveConfirmPop.Attributes.CssStyle.Add("display", "none");
        savePop.Attributes.CssStyle.Add("display", "none");
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

