<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Task2Form.aspx.cs" Inherits="ASP_NET.Task2Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>Task 2</title>
	<link rel="stylesheet" type="text/css" href="Style.css" />
</head>
<body>
	<main>
		<div class="border">
			<form runat="server" class="task2_grid">
				<asp:ListBox ID="listBox" RepeatDirection="Vertical" runat="server" />
				<div class="task2_buttons">
					<asp:Button runat="server" OnClick="OnClick_New" Text="New" />
					<asp:Button runat="server" OnClick="OnClick_Edit" Text="Edit" />
					<asp:Button runat="server" OnClick="OnClick_Remove" Text="Remove" />
				</div>
				<div class="task2_input">
					<asp:TextBox ID="textBox" runat="server" />
				</div>
			</form>
		</div>
	</main>
</body>
</html>
