<%@ Page Language="C#" AutoEventWireup="true" CodeFile="logIn.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="jscripts/jquery-1.12.0.min.js"></script>
    <script src="jscripts/myScript.js"></script>
    <link href="styles/mystyle.css" rel="stylesheet" />
    <title>כניסה לעורך</title>
</head>
<body>
    <header>
        <!--קישור לדף עצמו כדי להתחיל את המשחק מחדש בלחיצה על הלוגו-->
        <a href="index.html">
            <img id="logo" src="images/logo.png" style="width:0px" /> <!--הלוגו של המשחק שלכם-->
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
        <div id="aboutDiv" class="popUpAnimation bounceInDown hide">
            <a class="closeAbout">X</a>
            <p><img src="images/about.png" style="width:80%" /></p>
        </div>
        <div id="howToPlayDiv" class="popUpAnimation bounceInDown hide">
            <a class="closeHowToPlay">X</a>
            <p>בלחיצה על מקש ימני בעכבר בוחרים את התשובה הנכונה</p>
            <video id="myVideo" width="400" controls>
                <source src="images/howToPlay.mp4" type="video/mp4" />
            </video>
        </div>

    <form id="form1" runat="server">
        <div id="loginPage">
            <div id="bubble" class="bubble"></div>
             <asp:Label ID="Label1" runat="server" Text="התחברות לעורך"></asp:Label>
            <br/>
            <asp:Label ID="Label2" runat="server" Text="שם משתמש"></asp:Label>
             <br/>
            <asp:TextBox ID="usernameTB" item="1" cssClass="login" runat="server" style="background-color:#FFFFFF;"></asp:TextBox>
            <br/>
            <asp:Label ID="Label3" runat="server" Text="סיסמא"></asp:Label>
            <br/>
            <asp:TextBox ID="passwordTB" item="2" cssClass="login" runat="server" TextMode="Password"></asp:TextBox>
            <br/>
            <asp:Button ID="loginBtn" runat="server" Text="כניסה" OnClick="login_Click" />
            <asp:Label ID="wrongLoginTxt" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
