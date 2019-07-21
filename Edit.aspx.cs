using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing;

public partial class Edit : System.Web.UI.Page
{
    bool EditClick;
    int counter;
    string imagesLibPath = "imagesUploaded/";
    string imageNewName;
    string imageNewNameAns;

    protected void Page_Load(object sender, EventArgs e)
    {
        EditClick = Convert.ToBoolean(Session["EditClick"]);
        counter = Convert.ToInt16(Session["quesCount"]);

        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("trees/tree.xml"));
        string theItemId = Session["theItemIdSession"].ToString();
        XmlDataSource1.XPath = "/games/game[@gid = " + theItemId + "]/question";

        XmlNode node = myDoc.SelectSingleNode("/games/game[@gid=" + theItemId + "]");
        GameNameBox.Text = Server.UrlDecode(node.SelectSingleNode("gameName").InnerText);
        XmlNode counterNode = myDoc.SelectSingleNode("/games/game[@gid=" + theItemId + "]");
        counter = counterNode.SelectNodes("question").Count;
        quesCounter.Text = counter.ToString();

    }
    static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = Convert.ToInt32(imgPhoto.Width);
        int sourceHeight = Convert.ToInt32(imgPhoto.Height);

        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height,
                          PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
        grPhoto.Clear(System.Drawing.Color.White);
        grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
            new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            System.Drawing.GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string theItemId = Session["theItemIdSession"].ToString();

        ImageButton i = (ImageButton)e.CommandSource;
        string theId = i.Attributes["ItemId"];
        Session["ItemIdSessionEdit"] = i.Attributes["ItemId"];


        switch (e.CommandName)
        {

            case "deleteRow":
                deleteRow(theId, theItemId);
                break;

            case "editRow":

                editRow(theId, theItemId);
                break;
        }
    }


    void deleteRow(string theId, string theItemId)
    {

        popUpWindow.Style.Add("display", "block");

        popUp.Style.Add("display", "block");

    }
    void editRow(string theId, string theItemId)
    {
        EditClick = true;
        Session["EditClick"] = EditClick;
        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("trees/tree.xml"));
        QuestBox.Text = "";
        quesImageUpload.ImageUrl = "images/photo.svg";

        for (int i = 1; i <= 4; i++)
        {
            ((TextBox)FindControl("AnsrBox" + i)).Text = "";
            ((ImageButton)FindControl("ImageForUpAns" + i)).ImageUrl = "images/photo.svg";
        }

        XmlNodeList myques;
        myques = myDoc.SelectNodes("/games/game[@gid=" + theItemId + "]/question[@id=" + theId + "]");

        foreach (XmlNode mynode in myques)
        {
            QuestBox.Text += mynode.Attributes["text"].Value;

        }

        XmlNodeList myquesImg;
        myquesImg = myDoc.SelectNodes("/games/game[@gid=" + theItemId + "]/question[@id=" + theId + "]");

        foreach (XmlNode mynode in myquesImg)
        {
            if (mynode.Attributes["photo"].Value != "noImage")
            {

                quesImageUpload.ImageUrl = imagesLibPath + mynode.Attributes["photo"].Value;
            }
        }

        XmlNode countAns;
        countAns = myDoc.SelectSingleNode("/games/game[@gid=" + theItemId + "]/question[@id=" + theId + "]");
        int round = countAns.SelectNodes("answer").Count;
        round = round + 1;
        for (int a = 1; a <= 4; a++)
        {
            XmlNodeList myAns;
            myAns = myDoc.SelectNodes("/games/game[@gid=" + theItemId + "]/question[@id=" + theId + "]/answer[@id= " + a + "]");
            foreach (XmlNode mynode in myAns)
            {
                if (mynode.Attributes["type"].Value == "text")
                {
                    ((TextBox)FindControl("AnsrBox" + a)).Text += mynode.InnerXml;
                }
                else
                {
                    ((ImageButton)FindControl("ImageForUpAns" + a)).ImageUrl = imagesLibPath + mynode.InnerXml;
                }
            }
        }
    }

    protected void XmlDataSource1_Transforming(object sender, EventArgs e)
    {

    }

 

    protected void ExitBtn_Click(object sender, EventArgs e)
    {

        popUpWindow.Style.Add("display", "none");
        popUp.Style.Add("display", "none");
    }
    protected void DeleteFinal_Click(object sender, EventArgs e)
    {
        string theItemId = Session["theItemIdSession"].ToString();
        string editItemId = Session["ItemIdSessionEdit"].ToString();

        Button myDeleteBtn = (Button)sender;
        string ButtonID = myDeleteBtn.ID;
        string PopUpID = ButtonID.Substring(4);
        popUpWindow.Style.Add("display", "none");

        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        XmlNode node = Document.SelectSingleNode("/games/game[@gid=" + theItemId + "]/question[@id=" + editItemId + "]");
        node.ParentNode.RemoveChild(node);

        counter--;
        Session["quesCount"] = counter;
        quesCounter.Text = counter.ToString();
        XmlNode quesNum = Document.SelectSingleNode("/games/game[@gid=" + theItemId + "]");
        quesNum.Attributes["quesNum"].Value = counter.ToString();

        XmlDataSource1.Save();
        GridView1.DataBind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }

    protected void QuestSave(object sender, EventArgs e)
    {
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(Server.MapPath("trees/tree.xml"));

        string theItemId = Session["theItemIdSession"].ToString();
        XmlNode counterNode = myDoc.SelectSingleNode("/games/game[@gid=" + theItemId + "]");
        counter = counterNode.SelectNodes("question").Count;

        if (EditClick == true)
        {
            string editItemId = Session["ItemIdSessionEdit"].ToString();
            XmlNode quesNode = myDoc.SelectSingleNode("/games/game[@gid=" + theItemId + "]/question[@id=" + editItemId + "]");
            if (FileUpload1.HasFile)
            {
                string fileType = FileUpload1.PostedFile.ContentType;
                if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
                {
                    // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                    string fileName = FileUpload1.PostedFile.FileName;
                    // הסיומת של הקובץ
                    string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                    //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                    string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");

                    // חיבור השם החדש עם הסיומת של הקובץ
                    imageNewName = myTime + endOfFileName;


                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileUpload1.PostedFile.InputStream);

                    //קריאה לפונקציה המקטינה את התמונה
                    //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                    System.Drawing.Image objImage = FixedSize(bmpPostedImage, 300, 300);

                    //שמירה של הקובץ לספרייה בשם החדש שלו
                    objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);

                    //הצגה של הקובץ החדש מהספרייה
                    quesImageUpload.ImageUrl = imagesLibPath + imageNewName;

                    myDoc.Save(Server.MapPath("/trees/tree.xml"));

                    quesImageUpload.ImageUrl = null;

                    quesNode.Attributes["text"].Value = Server.UrlDecode(QuestBox.Text);
                    quesNode.Attributes["photo"].Value = "";
                    quesNode.Attributes["photo"].Value = imageNewName.ToString();

                    myDoc.Save(Server.MapPath("/trees/tree.xml"));
                }
                else
                {
                    //not an image
                }
            }
            else
            {
                quesNode.Attributes["text"].Value = Server.UrlDecode(QuestBox.Text);

                myDoc.Save(Server.MapPath("/trees/tree.xml"));
            }
            for (int i = 1; i <= 4; i++)
            {
                if (((FileUpload)FindControl("FileUploadAns" + i)).HasFile)
                {
                    string fileType = ((FileUpload)FindControl("FileUploadAns" + i)).PostedFile.ContentType;
                    if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
                    {
                        // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                        string fileName = ((FileUpload)FindControl("FileUploadAns" + i)).PostedFile.FileName;
                        // הסיומת של הקובץ
                        string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                        //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                        string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");


                        // חיבור השם החדש עם הסיומת של הקובץ
                        imageNewNameAns = myTime + i + endOfFileName;

                        System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(((FileUpload)FindControl("FileUploadAns" + i)).PostedFile.InputStream);

                        System.Drawing.Image objImage = FixedSize(bmpPostedImage, 300, 300);

                        objImage.Save(Server.MapPath(imagesLibPath) + imageNewNameAns);

                        ////שמירה של הקובץ לספרייה בשם החדש שלו
                        //((FileUpload)FindControl("FileUploadAns" + i)).PostedFile.SaveAs(Server.MapPath(imagesLibPath) + imageNewNameAns);

                        //הצגה של הקובץ החדש מהספרייה
                        ((ImageButton)FindControl("ImageForUpAns" + i)).ImageUrl = imagesLibPath + imageNewNameAns;

                        quesNode.ChildNodes[i - 1].InnerXml = imageNewNameAns;

                        myDoc.Save(Server.MapPath("/trees/tree.xml"));
                    }
                    else
                    {
                        // הקובץ אינו תמונה ולכן לא ניתן להעלות אותו
                    }
                }
                else
                {
                    if (((TextBox)FindControl("AnsrBox" + i)).Text != "")
                    {
                        quesNode.ChildNodes[i - 1].InnerXml = Server.UrlDecode(((TextBox)FindControl("AnsrBox" + i)).Text);

                        myDoc.Save(Server.MapPath("/trees/tree.xml"));
                    }
                    
                }
            }

            XmlDataSource1.Save();
            GridView1.DataBind();
        }
        else
        {
            Session["EditClick"] = false;
            counter++;
            Session["quesCount"] = counter;

            int idNewQues;
            XmlNode checkNode = myDoc.SelectSingleNode("/games/game[@gid=" + theItemId + "]/question");
            if (checkNode == null)
            {
                idNewQues = 1;
            }
            else
            {
                int idNew = Convert.ToInt16(myDoc.SelectSingleNode("/games/game[@gid=" + theItemId + "]/question[last()]/@id").Value);
                idNew++;
                idNewQues = idNew;
            }

            string QuestBox1 = Server.UrlDecode(QuestBox.Text);

            XmlNode quesNum = myDoc.SelectSingleNode("/games/game[@gid=" + theItemId + "]");
            quesNum.Attributes["quesNum"].Value = counter.ToString();

            XmlElement question = myDoc.CreateElement("question");
            question.SetAttribute("id", idNewQues.ToString());
            question.SetAttribute("text", QuestBox1.ToString());
            counterNode.AppendChild(question);

            if (FileUpload1.HasFile)
            {

                string fileType = FileUpload1.PostedFile.ContentType;
                if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
                {
                    // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                    string fileName = FileUpload1.PostedFile.FileName;
                    // הסיומת של הקובץ
                    string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                    //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                    string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");

                    // חיבור השם החדש עם הסיומת של הקובץ
                    imageNewName = myTime + endOfFileName;

                    //שמירה של הקובץ לספרייה בשם החדש שלו
                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileUpload1.PostedFile.InputStream);

                    //קריאה לפונקציה המקטינה את התמונה
                    //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                    System.Drawing.Image objImage = FixedSize(bmpPostedImage, 300, 300);

                    //שמירה של הקובץ לספרייה בשם החדש שלו
                    objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);

                    //הצגה של הקובץ החדש מהספרייה
                    quesImageUpload.ImageUrl = imagesLibPath + imageNewName;

                    question.SetAttribute("photo", imageNewName);

                    myDoc.Save(Server.MapPath("/trees/tree.xml"));

                    quesImageUpload.ImageUrl = null;
                }
                else
                {
                    // הקובץ אינו תמונה ולכן לא ניתן להעלות אותו
                }
            } else {

                question.SetAttribute("photo", "noImage");

                myDoc.Save(Server.MapPath("/trees/tree.xml"));

            }
            for (int i = 1; i <= 4; i++)
            {
                if (((FileUpload)FindControl("FileUploadAns" + i)).HasFile)
                {

                    string fileType = ((FileUpload)FindControl("FileUploadAns" + i)).PostedFile.ContentType;
                    if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
                    {
                        // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                        string fileName = ((FileUpload)FindControl("FileUploadAns" + i)).PostedFile.FileName;
                        // הסיומת של הקובץ
                        string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                        //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                        string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");

                        // חיבור השם החדש עם הסיומת של הקובץ
                        imageNewNameAns = myTime + i + endOfFileName;

                        System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(((FileUpload)FindControl("FileUploadAns" + i)).PostedFile.InputStream);

                        System.Drawing.Image objImage = FixedSize(bmpPostedImage, 300, 300);

                        objImage.Save(Server.MapPath(imagesLibPath) + imageNewNameAns);

                        ////שמירה של הקובץ לספרייה בשם החדש שלו
                        //((FileUpload)FindControl("FileUploadAns" + i)).PostedFile.SaveAs(Server.MapPath(imagesLibPath) + imageNewNameAns);

                        //הצגה של הקובץ החדש מהספרייה
                        ((ImageButton)FindControl("ImageForUpAns" + i)).ImageUrl = imagesLibPath + imageNewNameAns;


                        XmlElement newAns = myDoc.CreateElement("answer");
                        newAns.SetAttribute("id", i.ToString());
                        if (i == 1)
                        {
                            newAns.SetAttribute("correct", "y");
                        }
                        else
                        {
                            newAns.SetAttribute("correct", "n");
                        }
                        newAns.SetAttribute("type", "photo");
                        newAns.InnerXml = imageNewNameAns;
                        question.AppendChild(newAns);

                        myDoc.Save(Server.MapPath("/trees/tree.xml"));
                    }
                    else
                    {
                        // הקובץ אינו תמונה ולכן לא ניתן להעלות אותו
                    }
                }
                else
                {
                    if (((TextBox)FindControl("AnsrBox" + i)).Text != "")
                    {
                        XmlElement newAns = myDoc.CreateElement("answer");
                        newAns.SetAttribute("id", i.ToString());
                        if (i == 1)
                        {
                            newAns.SetAttribute("correct", "y");
                        }
                        else
                        {
                            newAns.SetAttribute("correct", "n");
                        }
                        newAns.SetAttribute("type", "text");
                        newAns.InnerXml = Server.UrlDecode(((TextBox)FindControl("AnsrBox" + i)).Text);
                        question.AppendChild(newAns);

                        myDoc.Save(Server.MapPath("/trees/tree.xml"));
                    }
                   
                }
            }

            XmlDataSource1.Save();
            GridView1.DataBind();
        }

        


        Session["EditClick"] = false;
        QuestBox.Text = "";
        quesImageUpload.ImageUrl = "images/photo.svg";
        for (int i = 1; i <= 4; i++)
        {
            ((TextBox)FindControl("AnsrBox" + i)).Text = "";
            ((ImageButton)FindControl("ImageForUpAns" + i)).ImageUrl = "images/photo.svg";
        }
        quesCounter.Text = counter.ToString();
    }

    protected void backBtn(object sender, EventArgs e)
    {
        Session["EditClick"] = false;
        Response.Redirect("MyGames.aspx");
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void publishBtn(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
        xmlDoc.Load(Server.MapPath("trees/tree.xml"));
        string theItemId = Session["theItemIdSession"].ToString();
        XmlDataSource1.XPath = "/games/game[@gid = " + theItemId + "]";

        XmlNode gamePublish = xmlDoc.SelectSingleNode("/games/game[@gid=" + theItemId + "]");
        gamePublish.Attributes["publish"].Value = "true";

        XmlDataSource1.Save();
        Session["theItemIdSession"] = false;

        Response.Redirect("MyGames.aspx?savePop=true");

    }
}