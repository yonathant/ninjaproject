<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyGames.aspx.cs" Inherits="MyGames" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>המשחקים שלי</title>

    <!-- הפניה לקובץ הCSS -->
    <link href="styles/mystyle.css" rel="stylesheet" />

    <!-- הפניה לקבצי הסקריפט. חשוב שיהיה את שניהם!! -->
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
        <div id="bubble" class="bubble"></div>
        <div id="gamePage" style="text-align: right;">
            <div id="topGamesSection">
            <asp:Label ID="Label1" runat="server" Text=" המשחקים שלי"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="יצירת משחק חדש"></asp:Label>
            <asp:TextBox ID="TextBox1" item="1" CharacterLimit="80" runat="server"></asp:TextBox>
            <div class="countHolder" style="margin-left:30px;">
            <asp:Label ID="LabelCounter1" runat="server" Text="0/80" />
            </div>
            <asp:ImageButton ID="addGameBtn" runat="server" ImageUrl="~/images/addDisabled.svg" OnClick="ImageButton1_Click" Enabled="False" />
            </div>
            <div class="myGames">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1" BorderColor="#CCCCCC" BorderWidth="1px" CellPadding="4" GridLines="Horizontal" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>

                    <asp:TemplateField HeaderText="שם המשחק">
                        <ItemTemplate>
                            <asp:Label ID="NameLabel" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "gameName")%>'> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="קוד המשחק">
                        <ItemTemplate>
                            <asp:Label ID="idLabel" runat="server" Text='<%# Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "@gid").ToString())%>'> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" שאלות">
                        <ItemTemplate>
                            <asp:Label ID="QuestLabel" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@quesNum")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="עריכה">
                        <ItemTemplate>
                            <asp:ImageButton ID="editImageButton" CommandName="editRow" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@gid")%>' runat="server" ImageUrl="~/images/edit-3.svg" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="מחיקה">
                        <ItemTemplate>
                            <asp:ImageButton ID="deleteImageButton" CommandName="deleteRow" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@gid")%>' runat="server" ImageUrl="~/images/trash-2.svg" />
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="פרסום">
                        <ItemTemplate>
                        <div id="ttip">
                            <b class="ttipTxt">דרושות 10 שאלות בכדי לפרסם משחק</b>
                            <asp:CheckBox ID="isPassCheckBox" runat="server" AutoPostBack="true" Class='<%#CheckIfCanPublish(XPathBinder.Eval(Container.DataItem,"@gid").ToString())%>' OnCheckedChanged="isPassCheckBox_CheckedChanged" Checked='<%#Convert.ToBoolean(XPathBinder.Eval(Container.DataItem,"@publish"))%>' theItemId='<%#XPathBinder.Eval(Container.DataItem,"@gid")%>'/>
                        </div>
                        </ItemTemplate>

                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#ffdf40" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" ForeColor="Black" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" ForeColor="Black" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
            </div>

            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/trees/tree.xml" XPath="/games/game"></asp:XmlDataSource>

             <asp:Panel ID="popUpWindow" runat="server">
                    <asp:Panel ID="popUp" CssClass="PopUp" runat="server">
                    <asp:Label ID="Label3" runat="server" Text="האם אתה בטוח שברצונך למחוק משחק זה ?"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text="לא ניתן לשחזר משחק לאחר מחיקתו"></asp:Label>
                    <!-- כפתור יציאה - יש לשים לב שהוא מפנה בלחיצה לאותה פונקציה של הכפתור יציאה השני -->
                    <asp:Button ID="dontDelete" CssClass="popUpBtn" runat="server" Text="לא" OnClick="ExitBtn_Click" />
                    <asp:Button ID="delete" CssClass="popUpBtn" runat="server" Text="כן" OnClick="DeleteFinal_Click" />
                </asp:Panel>

            </asp:Panel>
             <asp:Panel ID="saveConfirmPop" runat="server">
                <asp:Panel ID="savePop" CssClass="PopUp" runat="server">
                    <asp:Label ID="saveConfTxt" runat="server" Text="המשחק פורסם בהצלחה!"></asp:Label>
                    <asp:Button ID="saveConfBtn" CssClass="popUpBtn" runat="server" Text="המשך" OnClick="saveConfBtn_btn" />
                </asp:Panel>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
