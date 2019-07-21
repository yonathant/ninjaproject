<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>
<!DOCTYPE html>

<html dir="rtl" lang="heb" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>עריכת משחק</title>
        <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=yes" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
            <%--CSS--%>
 <link href="styles/mystyle.css" rel="stylesheet" />
    <%--Scripts--%>
    <script src="jscripts/jquery-1.12.0.min.js"></script>
    <script src="jscripts/myScript.js"></script>
</head>
<body>
    <header>
            <!--קישור לדף עצמו כדי להתחיל את המשחק מחדש בלחיצה על הלוגו-->
            <a href="index.html">
                <img id="logo" src="images/logo.png" style="width:0px"/> <!--הלוגו של המשחק שלכם-->
                <p>ידענינג'ה</p>
            </a>
            <!--תפריט הניווט בראש העמוד-->
            <nav>
                <ul>
                    <li><a class="about">אודות</a></li>
                    <li><a class="howToPlay">איך משחקים?</a></li>
                    <li><a href="login.aspx" class="editor">עורך</a></li>
                </ul>
            </nav>
        </header>
        <div id="aboutDiv" class="popUpAnimation bounceInDown hide">
            <a class="closeAbout">X</a>
            <p><img src="images/about.png" style="width:80%" /></p>
        </div>
        <div id="howToPlayDiv" class="popUpAnimation bounceInDown hide">
            <a class="closeHowToPlay">X</a>
            <p>בלחיצה על מקש ימני בעכבר בוחרים את התשובה הנכונה</p>
            <video id="myVideo" width="400" controls autoplay="autoplay">
                <source src="images/howToPlay.mp4" type="video/mp4" />
            </video>
        </div>

    <form id="form1" runat="server">
      <div id="bubble"></div>

      <div id="editPage">
        <div id="editRightDiv" style="text-align: right; display:inline-block;" >
            <div id="editTopSection">
                <asp:Label ID="Label1" runat="server" Text="שם המשחק" />
                <asp:TextBox ID="GameNameBox" item="2" CssClass="CharacterCount" CharacterLimit="60" runat="server"></asp:TextBox>
                <asp:Label ID="LabelCounter2" runat="server" Text="0/60" />
            </div>

            <asp:Label ID="quesLabel" runat="server" Text="שאלה" />
            <asp:TextBox ID="QuestBox" runat="server" item="3" CssClass="CharacterCount" CharacterLimit="80"></asp:TextBox>
            <div class="countHolder">
            <asp:Label ID="LabelCounter3" runat="server" Text="0/80" />
            </div>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:ImageButton ID="quesImageUpload" runat="server" ImageUrl="~/images/photo.svg" onClientClick="openFileUploader1(); return false;"/>
            <asp:ImageButton ID="quesDeleteImg" runat="server" ImageUrl="~/images/trash-2.svg" onClientClick="quesImgDeleter(); return false;"/>
            <br />

            <asp:Label ID="ansInfoLabel" runat="server" Text=" הזן לפחות 2 אפשרויות מענה"></asp:Label>

            <div class="ansLine">
            <asp:TextBox id="AnsrBox1" item="4" CssClass="CharacterCount" CharacterLimit="40" placeholder="הכנס תשובה נכונה" runat="server"/>
            <div class="countHolder">
            <asp:Label ID="LabelCounter4" runat="server" Text=" 0/40"></asp:Label>
            </div>
            <asp:Label ID="Label8" runat="server" Text=" או "></asp:Label>
            <asp:FileUpload ID="FileUploadAns1" runat="server" />
            <asp:ImageButton ID="ImageForUpAns1" runat="server" ImageUrl="~/images/photo.svg" onClientClick="openFileUploader2(); return false;"/>
            <asp:ImageButton CssClass="ansDeleteImg" runat="server" ImageUrl="~/images/trash-2.svg" onClientClick="ansImgDeleter1(); return false;"/>
            </div>

            <div class="ansLine">
            <asp:TextBox id="AnsrBox2" item="5" CssClass="CharacterCount" CharacterLimit="40" placeholder="הכנס תשובה" runat="server"/>
            <div class="countHolder">
            <asp:Label ID="LabelCounter5" runat="server" Text=" 0/40"></asp:Label>
            </div>
            <asp:Label ID="Label11" runat="server" Text=" או "></asp:Label>
            <asp:FileUpload ID="FileUploadAns2" runat="server" />
            <asp:ImageButton ID="ImageForUpAns2" runat="server" ImageUrl="~/images/photo.svg" onClientClick="openFileUploader3(); return false;"/> 
            <asp:ImageButton CssClass="ansDeleteImg" runat="server" ImageUrl="~/images/trash-2.svg" onClientClick="ansImgDeleter2(); return false;"/>
            </div>

            <div class="ansLine">
            <asp:TextBox id="AnsrBox3" item="6" CssClass="CharacterCount" CharacterLimit="40" placeholder="הכנס תשובה" runat="server"/>
            <div class="countHolder">
            <asp:Label ID="LabelCounter6" runat="server" Text=" 0/40"></asp:Label>
            </div>
            <asp:Label ID="Label14" runat="server" Text=" או "></asp:Label>
            <asp:FileUpload ID="FileUploadAns3" runat="server" />
            <asp:ImageButton ID="ImageForUpAns3" runat="server" ImageUrl="~/images/photo.svg" onClientClick="openFileUploader4(); return false;"/>   
            <asp:ImageButton CssClass="ansDeleteImg" runat="server" ImageUrl="~/images/trash-2.svg" onClientClick="ansImgDeleter3(); return false;"/>
            </div>

            <div class="ansLine">
            <asp:TextBox id="AnsrBox4" item="7" CssClass="CharacterCount" CharacterLimit="40" placeholder="הכנס תשובה" runat="server" Enabled="False"/>
            <div class="countHolder">
            <asp:Label ID="LabelCounter7" runat="server" Text=" 0/40"></asp:Label>
            </div>
            <asp:Label ID="Label17" runat="server" Text=" או "></asp:Label>
            <asp:FileUpload ID="FileUploadAns4" runat="server" />
            <asp:ImageButton ID="ImageForUpAns4" runat="server" ImageUrl="~/images/photo.svg" onClientClick="openFileUploader5(); return false;"/> 
            <asp:ImageButton CssClass="ansDeleteImg" runat="server" ImageUrl="~/images/trash-2.svg" onClientClick="ansImgDeleter4(); return false;"/>
            </div>

            <div id="btnContainer">
                <asp:Button ID="publish" runat="server" Text="פרסם" Enabled="False" OnClick="publishBtn" />
                <asp:Button ID="saveQuest" runat="server" Text="שמור שאלה" OnClick="QuestSave" Enabled="False" />
                <asp:Button ID="cancleBack" runat="server" Text="חזור" OnClick="backBtn" />
            </div>
        </div>

       <div id="editLeftDiv">
       <asp:Label ID="quesCounter" runat="server" Text="" />
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1" BorderColor="#CCCCCC" BorderWidth="0.5px" CellPadding="4" GridLines="Horizontal" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField HeaderText="שאלות - 10/">
                    <ItemTemplate>
                        <asp:Label ID="question" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@text")%>'> </asp:Label>     
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="עריכה">
                    <ItemTemplate>
                        <asp:ImageButton ID="editImageButton" CssClass="insideEditBtn" CommandName="editRow" ItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/edit-3.svg" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="מחיקה">
                    <ItemTemplate>
                        <asp:ImageButton ID="deleteImageButton" CommandName="deleteRow" ItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/trash-2.svg" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            </asp:GridView>
           </div>

             <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/trees/tree.xml" XPath="/games/game[@gid]/question" OnTransforming="XmlDataSource1_Transforming"></asp:XmlDataSource>
             <asp:Panel ID="popUpWindow" runat="server">
                <asp:Panel ID="popUp" CssClass="PopUp" runat="server">
                    <asp:Label ID="Label3" runat="server" Text="האם אתה בטוח שברצונך למחוק שאלה זו ?"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text="לא ניתן לשחזר שאלה לאחר מחיקתה"></asp:Label>
                    <!-- כפתור יציאה - יש לשים לב שהוא מפנה בלחיצה לאותה פונקציה של הכפתור יציאה השני -->
                    <asp:Button ID="dontDelete" CssClass="popUpBtn" runat="server" Text="לא" OnClick="ExitBtn_Click" />
                    <asp:Button ID="delete" CssClass="popUpBtn" runat="server" Text="כן" OnClick="DeleteFinal_Click" />
                </asp:Panel>
            </asp:Panel>
      </div>
    </form>
</body>
</html>
