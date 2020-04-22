<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Task4Form.aspx.cs" Inherits="ASP_NET.Task4Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Task 4</title>
    <link rel="stylesheet" type="text/css" href="Style.css?v=2" />
</head>
<body>
    <main class="main">
        <div class="border">
            <form runat="server" class="task4_grid">
                <div class="t4_grid_2row2colum">
                    <label for="loginField" class="loginField">Логин</label>
                    <asp:TextBox ID="loginField" runat="server"></asp:TextBox>
                    <label for="passwordField" class="passwordField">Пароль</label>
                    <asp:TextBox ID="passwordField" runat="server"></asp:TextBox>
                </div>
                <div class="t4_grid_3colum">
                    <label>Пол</label>
                    <div>
                        <asp:RadioButton ID="radioBtnMale" GroupName="gender" runat="server" />
                        <label for="radioBtnMale">М</label>
                    </div>
                    <div>
                        <asp:RadioButton ID="radioBtnFemale" GroupName="gender" runat="server" />
                        <label for="radioBtnFemale">Ж</label>
                    </div>
                </div>
                <div class="t4_grid_4row">
                    <label>Как о нас узнали?</label>
                    <div>
                        <asp:CheckBox ID="from1" Text="Реклама" runat="server" />
                    </div>
                    <div>
                        <asp:CheckBox ID="from2" Text="Рекомендация" runat="server" />
                    </div>
                    <div>
                        <asp:CheckBox ID="from3" Text="В поисковике" runat="server" />
                    </div>
                </div>
                <div class="t4_grid_2row">
                    <label>Несколько слов о себе:</label>
                    <asp:TextBox ID="aboutUser" TextMode="MultiLine" runat="server" />
                </div>
                <asp:Button ID="regBtn" OnClick="regBtn_OnClick" runat="server" Text="Зарегестрироваться" CssClass="btn" />
            </form>
        </div>
    </main>
</body>
</html>
