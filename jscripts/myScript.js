function openFileUploader1() {
    $('#FileUpload1').click();
}
function openFileUploader2() {
    $('#FileUploadAns1').click();
}
function openFileUploader3() {
    $('#FileUploadAns2').click();
}
function openFileUploader4() {
    $('#FileUploadAns3').click();
}
function openFileUploader5() {
    $('#FileUploadAns4').click();
}
function quesImgDeleter() {
    $('#quesImageUpload').attr('src', 'images/photo.svg');
}
function ansImgDeleter1() {
    $("#ImageForUpAns1").attr('src', 'images/photo.svg');
}
function ansImgDeleter2() {
    $("#ImageForUpAns2").attr('src', 'images/photo.svg');
}
function ansImgDeleter3() {
    $("#ImageForUpAns3").attr('src', 'images/photo.svg');
}
function ansImgDeleter4() {
    $("#ImageForUpAns4").attr('src', 'images/photo.svg');
}
var savePopUp;

//כאשר העמוד נטען
$(document).ready(function () {
    if ($("#bubble").hasClass('bubble') == false) {
        enableBtn();
    }

    if ($("#quesCounter").text() == "10") {
        $("#publish").attr("disabled", false);
    }

    $(".about").click(function () {
        $("#aboutDiv").toggle();
    });

    $(".howToPlay").click(function () {
        $("#howToPlayDiv").toggle();
    });

    $(".closeAbout").click(function () {
        $("#aboutDiv").hide();
        $("#gameIframe")[0].contentWindow.focus();
    });

    $(".closeHowToPlay").click(function () {
        $("#howToPlayDiv").hide();
        $("#gameIframe")[0].contentWindow.focus();
    });


    $("td #ttip").mouseover(function () {
        if ($("span", this).hasClass('disabled')) {
            $(".ttipTxt", this).css("display", "block");
        }
    });
    $("td #ttip").mouseleave(function () {
        $(".ttipTxt", this).css("display", "none");
    });
    //תעבור על כל אלמנט שיש לו את הקלאס הזה       
    $(".disabled").each(function (e) {

        //תגדיר לו שיהיה לא מאופשר              
        $(this).find("input").attr("disabled", true);

        //תבטל את תיבת הסימון במידה ויש              
        $(this).find("input").attr("checked", false);

    });
    //בהקלדה בתיבת הטקסט
    $(".CharacterCount").keyup(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
        enableBtn();
    });

    //בהעתקה של תוכן לתיבת הטקסט
    $(".CharacterCount").on("paste", function () {
        checkCharacter($(this));//קריאה לפונקציה שבודקת את מספר התווים
        enableBtn();
    });
    $("#TextBox1").keyup(function () {
        checkCharacter($(this));
        addGameFunc($(this));
    });
    $("#TextBox1").on("paste", function () {
        checkCharacter($(this));
        addGameFunc($(this));
    });
    $(".login").keyup(function () {
        enableBtnLogin($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });


    function enableBtnLogin(loginTxt) {
        var loginBtn = document.getElementById('loginBtn');
        if (loginTxt.val().length == 0) {
            loginBtn.disabled = true;
        } else {
            loginBtn.disabled = false;
        }
    }

    //פונקציה שמקבלת את תיבת הטקסט שבה מקלידים ובודקת את מספר התווים
    function checkCharacter(myTextBox) {

        //משתנה למספר התווים הנוכחי בתיבת הטקסט
        var countCurrentC = myTextBox.val().length;

        //משתנה המקבל את מספר תיבת הטקסט 
        var itemNumber = myTextBox.attr("item");

        //משתנה המכיל את מספר התווים שמוגבל לתיבה זו
        var CharacterLimitNum = myTextBox.attr("CharacterLimit");

        //בדיקה האם ישנה חריגה במספר התווים
        if (countCurrentC > CharacterLimitNum) {

            //מחיקת התווים המיותרים בתיבה
            myTextBox.val(myTextBox.val().substring(0, CharacterLimitNum));
            //עדכון של מספר התווים הנוכחי
            countCurrentC = CharacterLimitNum;

        }

        //הדפסה כמה תווים הוקלדו מתוך כמה
        $("#LabelCounter" + itemNumber).text(countCurrentC + "/" + CharacterLimitNum);
    }
    
    function addGameFunc(txt) {
        var addGame = document.getElementById('addGameBtn');
            if (txt.val().length == 0) {
                addGame.disabled = true;
                $("#addGameBtn").attr('src', 'images/addDisabled.svg');
            }
            else {
                addGame.disabled = false;
                $("#addGameBtn").attr('src', 'images/add.svg');
            }
    }

    //לאחר שלחצנו על התמונה שרצינו לבחור-גזע השאלה 
    $("#FileUpload1").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#quesImageUpload').attr('src', e.target.result);
            }
            reader.readAsDataURL(this.files[0]);
        }
    });

    //    //לאחר שלחצנו על התמונה שרצינו לבחור-מסיחים  
    $("#FileUploadAns1").change(function () {
        $('#AnsrBox1').val("");
        $('#AnsrBox1').attr("disabled", true);
        $('#AnsrBox2').val("");
        $('#AnsrBox2').attr("disabled", true);
        $('#AnsrBox3').val("");
        $('#AnsrBox3').attr("disabled", true);
        $('#AnsrBox4').val("");
        $('#AnsrBox4').attr("disabled", true);

        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#ImageForUpAns1').attr('src', e.target.result);
                enableBtn()
            }

            reader.readAsDataURL(this.files[0]);
        }
    });


    $("#FileUploadAns2").change(function () {
        $('#AnsrBox1').val("");
        $('#AnsrBox1').attr("disabled", true);
        $('#AnsrBox2').val("");
        $('#AnsrBox2').attr("disabled", true);
        $('#AnsrBox3').val("");
        $('#AnsrBox3').attr("disabled", true);
        $('#AnsrBox4').val("");
        $('#AnsrBox4').attr("disabled", true);

        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#ImageForUpAns2').attr('src', e.target.result);
                enableBtn()
            }

            reader.readAsDataURL(this.files[0]);

        }
    });


    $("#FileUploadAns3").change(function () {
        $('#AnsrBox1').val("");
        $('#AnsrBox1').attr("disabled", true);
        $('#AnsrBox2').val("");
        $('#AnsrBox2').attr("disabled", true);
        $('#AnsrBox3').val("");
        $('#AnsrBox3').attr("disabled", true);
        $('#AnsrBox4').val("");
        $('#AnsrBox4').attr("disabled", true);

        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#ImageForUpAns3').attr('src', e.target.result);
                enableBtn()
            }

            reader.readAsDataURL(this.files[0]);
        }
    });

    $("#FileUploadAns4").change(function () {
        $('#AnsrBox1').val("");
        $('#AnsrBox1').attr("disabled", true);
        $('#AnsrBox2').val("");
        $('#AnsrBox2').attr("disabled", true);
        $('#AnsrBox3').val("");
        $('#AnsrBox3').attr("disabled", true);
        $('#AnsrBox4').val("");
        $('#AnsrBox4').attr("disabled", true);

        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#ImageForUpAns4').attr('src', e.target.result);
                enableBtn()
            }

            reader.readAsDataURL(this.files[0]);
        }
    });

    function enableBtn() {

        $("#saveQuest").attr("disabled", true);

        //if text
        if ($("#AnsrBox1").val().length != 0 && $("#AnsrBox2").val().length != 0 && $("#QuestBox").val().length != 0) {
            $("#saveQuest").attr("disabled", false);

            $("#AnsrBox3").attr("disabled", false);
            if (($("#AnsrBox3").val().length != 0)) {
                $("#AnsrBox4").attr("disabled", false);
            }
            else {

                $("#AnsrBox4").attr("disabled", true);
            }

        }

        else {
            //if photo
            
            $("#AnsrBox3").attr("disabled", true);
            $("#AnsrBox4").attr("disabled", true);

            if ($("#ImageForUpAns1").attr('src').includes("images/photo.svg") == false && $("#ImageForUpAns2").attr('src').includes("images/photo.svg") == false && $("#QuestBox").val().length != 0) {
                $("#AnsrBox1").attr("disabled", true);
                $("#AnsrBox2").attr("disabled", true);
                $("#AnsrBox3").attr("disabled", true);
                $("#AnsrBox4").attr("disabled", true);


                $("#saveQuest").attr("disabled", false);
                $("#ImageForUpAns3").attr("disabled", false);


                if ($("#ImageForUpAns3").attr('src').includes("images/photo.svg") == false) {
                    $("#ImageForUpAns4").attr("disabled", false);

                }
                else {
                    $("#ImageForUpAns4").attr("disabled", true);

                }
            }
            else {
                $("#ImageForUpAns4").attr("disabled", true);
                $("#ImageForUpAns3").attr("disabled", true);
            }

        }

    }

});

